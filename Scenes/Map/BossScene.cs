using MagicShot_Roulette.Datas.Battle;
using MagicShot_Roulette.Datas.Stats;
using MagicShot_Roulette.Managers;
using MagicShot_Roulette.Utils;

namespace MagicShot_Roulette.Scenes.Map
{
    public class BossScene : BaseScene
    {
        private readonly GameManager manager;
        SelectVisualizer select;

        public BossScene() : base(SceneType.Boss)
        {
            manager = GameManager.GetInstance();
            select = new SelectVisualizer();
            RefreshSelectMenu();
        }

        public override void Display()
        {
            BattleVisualizer.Display(manager.Battle.State);
            Console.WriteLine(DisplayHelper.DoubleLine);
            select.Visualize();
        }

        public override void OnEnable()
        {
            BattleVisualizer.IsGunReversed = false;
            Enemy enemy = Enemy.GetEnemy(true);
            manager.Battle.StartBattle(enemy);
            RefreshSelectMenu();
            manager.Input.IsFalseInput = false;
        }

        public override void OnDisable()
        {
            manager.Input.IsFalseInput = false;
            manager.Save.Player.GetProgressionData().FloorIndex++;
        }

        public override void Update(KeyInfo key)
        {
            switch (key)
            {
                case KeyInfo.Up:
                    select.PrevSelect();
                    break;
                case KeyInfo.Down:
                    select.NextSelect();
                    break;
                case KeyInfo.Space:
                    int index = select.GetSelect();
                    if (index == 1)
                    {
                        UseSkill();
                        return;
                    }
                    if (index > 1)
                    {
                        UseItem(index);
                        return;
                    }
                    break;
            }

            BattleResult result = manager.Battle.State.Phase == BattlePhase.WaitingForPlayer ? HandlePlayerInput(key) : manager.Battle.PerformPhase();

            if (result is not null)
            {
                ApplyResult(result);
            }
        }

        private void UseItem(int index)
        {
            Inventory inven = manager.Save.Player.GetInven();
            int itemIndex = index - 2;
            if (itemIndex < 0 || itemIndex >= inven.GetCount())
            {
                manager.Battle.State.LastMessage = "사용할 아이템이 없습니다.";
                return;
            }

            Item item = inven.GetIndex(itemIndex);
            switch (item.type)
            {
                case ItemType.None:
                    manager.Battle.State.LastMessage = "사용할 수 없는 아이템입니다.";
                    break;
                case ItemType.Rope:
                    manager.Battle.SkipNextEnemyAttack();
                    break;
                case ItemType.Wine:
                    manager.Battle.RemoveCurrentBullet();
                    break;
                case ItemType.Quartz:
                    manager.Battle.PeekCurrentBullet();
                    break;
                case ItemType.Potion:

                    if (manager.Save.Player.GetBattleStat().HP < manager.Save.Player.GetBattleStat().MaxHP)
                    {
                        manager.Battle.UsePotion(1);
                    }
                    else
                    {
                        Console.WriteLine("HP가 이미 가득 차있습니다.");
                        Thread.Sleep(2000);
                        return;
                    }
                    break;
                case ItemType.MagicPowder:
                    manager.Battle.EmpowerNextShot();
                    break;
                case ItemType.Shield:
                    manager.Battle.BlockNextEnemyAttack();
                    break;
                case ItemType.ReverseRune:
                    if (manager.Battle.ReverseCurrentBullet())
                    {
                        BattleVisualizer.IsGunReversed = true;
                    }
                    break;
                case ItemType.Count:
                    manager.Battle.State.LastMessage = "사용할 수 없는 아이템입니다.";
                    break;
            }

            if (item.type != ItemType.None && item.type != ItemType.Count)
            {
                inven.Remove(item);
                RefreshSelectMenu();
            }
        }


        private void UseSkill()
        {
            if (manager.Save.Player.GetBattleStat().MP == 0)
            {
                manager.Battle.State.LastMessage = "MP가 부족합니다.";
                return;
            }
            switch (manager.Save.Player.Class)
            {
                case Datas.Stats.ClassType.Knight:
                    manager.Battle.BlockNextEnemyAttack();
                    manager.Battle.State.LastMessage = "스킬: 실드를 사용했습니다. 다음 몬스터의 공격을 방어합니다.";
                    break;
                case Datas.Stats.ClassType.Magician:
                    bool isReversed = manager.Battle.ReverseCurrentBullet();
                    BattleVisualizer.IsGunReversed = isReversed;
                    manager.Battle.State.LastMessage = isReversed
                        ? "스킬: 반전을 사용했습니다. 현재 약실의 탄환을 반전했습니다."
                        : "스킬: 반전을 사용했지만 반전할 탄환이 없습니다.";
                    break;
            }
            manager.Save.Player.GetBattleStat().MP--;

        }

        private void RefreshSelectMenu()
        {
            select.Clear();
            select.AddData("사격한다.");
            if (manager.Save.Player.Class == Datas.Stats.ClassType.Knight)
            {
                select.AddData("스킬: 실드 | 다음 몬스터의 공격을 방어");
            }
            else
            {
                select.AddData("스킬: 반전 | 현재 약실의 탄환을 반전");
            }

            Inventory inven = manager.Save.Player.GetInven();
            for (int i = 0; i < inven.GetCount(); i++)
            {
                Item item = inven.GetIndex(i);
                select.AddData(item.Name + " | " + item.Description);
            }
        }

        private BattleResult HandlePlayerInput(KeyInfo key)
        {
            switch (key)
            {
                case KeyInfo.Cancle:
                    return new BattleResult(BattlePhase.Ended, false, true, "스킵합니다.");
                case KeyInfo.Left:
                    return manager.Battle.SelectTarget(BattleTarget.Self);
                case KeyInfo.Right:
                    return manager.Battle.SelectTarget(BattleTarget.Enemy);
                case KeyInfo.Space:
                    BattleVisualizer.IsGunReversed = false;
                    return manager.Battle.ConfirmPlayerShot();
                default:
                    return null;
            }
        }

        private void ApplyResult(BattleResult result)
        {
            if (result.IsBattleOver)
            {
                manager.Input.IsFalseInput = false;
                Console.WriteLine(GetBattleOverMessage(result.Message));
                Thread.Sleep(2000);
                manager.Scene.ChangeScene(SceneType.Map);
                return;
            }

            manager.Input.IsFalseInput = !result.RequiresInput;
        }

        private string GetBattleOverMessage(string message)
        {
            if (IsPlayerWin())
            {
                string upgradeMessage = manager.Save.Player.UpgradeRandomStat();
                return $"{message}\n{upgradeMessage}";
            }

            return message;
        }

        private bool IsPlayerWin()
        {
            BattleStat playerStat = manager.Battle.State.Player.GetStatData().GetBattleStat();
            BattleStat enemyStat = manager.Battle.State.Enemy.GetStatData().GetBattleStat();
            return playerStat.IsAlive() && !enemyStat.IsAlive();
        }
    }
}
