using MagicShot_Roulette.Datas.Battle;
using MagicShot_Roulette.Datas.Stats;
using MagicShot_Roulette.Managers;
using MagicShot_Roulette.Utils;

namespace MagicShot_Roulette.Scenes.Map
{
    public class RestScene : BaseScene
    {
        GameManager manager;
        SelectVisualizer select;
        Random random;
        int cost;
        string message;
        bool isForged = false;

        public RestScene() : base(SceneType.Rest)
        {
            manager = GameManager.GetInstance();
            select = new SelectVisualizer();
            random = new Random();
            cost = 1;
            message = ""; 
            isForged = false;

            RefreshSelectMenu();
        }

        public override void Display()
        {
            DisplayHelper.DisplayPlayerInfo(manager.Save.Player);
            Console.WriteLine(DisplayHelper.DoubleLine);

            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message);
                Console.WriteLine(DisplayHelper.DoubleLine);
            }

            select.Visualize();
        }

        public override void OnDisable()
        {
        }

        public override void OnEnable()
        {
            cost = 1;
            message = "";
            RefreshSelectMenu();
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
                    SelectHandler(select.GetSelect());
                    break;
            }
        }

        private void RefreshSelectMenu()
        {
            select.Clear();
            select.AddData("강화(1회만) | MaxHP+ 1");
            select.AddData("강화(1회만) | MaxMP+ 1");
            select.AddData("강화(1회만) | 인벤토리크기+ 1");
            select.AddData($"아이템 뽑기 | {cost} 골드 소모");
            select.AddData($"진행하기(HP,MP가 모두 회복됩니다.)");

            if (isForged)
            {
                DisableUpgradeSelects();
            }
        }

        private void SelectHandler(int index)
        {
            switch (index)
            {
                case 0:
                    UpgradePlayer(PlayerUpgradeType.HP); 
                    break;
                case 1:
                    UpgradePlayer(PlayerUpgradeType.MP);
                    break;
                case 2:
                    UpgradePlayer(PlayerUpgradeType.MaxItem); 
                    break;
                case 3:
                    DrawItem();
                    break;
                case 4:
                    manager.Save.Player.RecoverAll();
                    manager.Scene.ChangeScene(SceneType.Map);
                    break;
            }
        }
        private void UpgradePlayer(PlayerUpgradeType upgrade)
        {
            message = manager.Save.Player.UpgradeStat(upgrade);
            isForged = true;
            DisableUpgradeSelects(); 
        }
        private void DisableUpgradeSelects()
        {
            select.Disable(0);
            select.Disable(1);
            select.Disable(2);
        }

        private void DrawItem()
        {
            PlayerStat player = manager.Save.Player;
            Inventory inven = player.GetInven();

            if (player.Money < cost)
            {
                message = "소지금이 부족합니다.";
                return;
            }

            if (inven.GetCount() >= inven.Capacity)
            {
                message = "인벤토리 크기가 부족합니다.";
                return;
            }

            message = "아이템을 뽑았습니다.";
            Item item = Item.Create((ItemType)random.Next(1, (int)ItemType.Count));
            inven.Add(item);
            player.Money -= cost;
            cost++;
            RefreshSelectMenu(); 
        }
    }
}
