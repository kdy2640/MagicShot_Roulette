using MagicShot_Roulette.Datas.Stats;
using MagicShot_Roulette.Managers;

namespace MagicShot_Roulette.Datas.Battle
{
    public enum BattlePhase
    {
        WaitingForPlayer,
        PlayerShot,
        EnemyAttack,
        Ended
    }

    public enum BattleTarget
    {
        Self,
        Enemy
    }

    public record BattleResult(BattlePhase Phase, bool RequiresInput, bool IsBattleOver, string Message);

    public class BattleState
    {
        public Player Player { get; set; }
        public Enemy Enemy { get; set; }
        public GunInfo Gun { get; set; }
        public BattleTarget SelectedTarget { get; set; } = BattleTarget.Enemy;
        public BattlePhase Phase { get; set; } = BattlePhase.WaitingForPlayer;
        public string LastMessage { get; set; } = "";
    }

    public class BattleManager
    {
        const int DefaultShotCount = 5;
        const int EnemyAttackDamage = 1;

        readonly PlayerStat playerStat;
        bool isNextShotEmpowered;
        bool isNextEnemyAttackBlocked;
        bool isNextEnemyAttackSkipped;

        GameManager manager;
        public BattleState State { get; } = new BattleState();

        public BattleManager(PlayerStat playerStat)
        {
            this.playerStat = playerStat;
            manager = GameManager.GetInstance();
        }

        public void StartBattle(Enemy enemy)
        {
            State.Player = new Player(playerStat);
            State.Enemy = enemy;
            State.Gun = CreateGun();
            State.SelectedTarget = BattleTarget.Enemy;
            State.Phase = BattlePhase.WaitingForPlayer;
            State.LastMessage = $"{GetName(enemy)}이 나타났다!";
            isNextShotEmpowered = false;
            isNextEnemyAttackBlocked = false;
            isNextEnemyAttackSkipped = false;
        }

        public BattleResult SelectTarget(BattleTarget target)
        {
            if (State.Phase == BattlePhase.WaitingForPlayer)
            {
                State.SelectedTarget = target;
                State.LastMessage = target == BattleTarget.Enemy ? "몬스터를 조준합니다." : "자신을 조준합니다.";
            }

            return Result(State.LastMessage);
        }

        public BattleResult ConfirmPlayerShot()
        {
            if (State.Phase != BattlePhase.WaitingForPlayer)
            {
                return Result(State.LastMessage);
            }

            State.Phase = BattlePhase.PlayerShot;
            return PerformPhase();
        }

        public BattleResult PerformPhase()
        {
            switch (State.Phase)
            {
                case BattlePhase.WaitingForPlayer:
                case BattlePhase.Ended:
                    return Result(State.LastMessage);
                case BattlePhase.PlayerShot:
                    return PlayerShot();
                case BattlePhase.EnemyAttack:
                    return EnemyAttack();
                default:
                    return Result(State.LastMessage);
            }
        }

        public void UsePotion(int amount)
        {
            State.Player.GetStatData().GetBattleStat().HP += amount;
            State.LastMessage = $"포션을 사용했습니다. HP가 {amount} 회복되었습니다.";
        }

        public ShotInfo RemoveCurrentBullet()
        {
            ShotInfo shot = State.Gun.RemoveCurrentShot();
            State.LastMessage = shot == ShotInfo.None ? "제거할 탄이 없습니다." : "와인을 사용했습니다. 현재 약실의 탄을 제거했습니다.";
            return shot;
        }

        public ShotInfo PeekCurrentBullet()
        {
            ShotInfo shot = State.Gun.PeekCurrentShot(); 
                
            switch(shot)
            {
                case ShotInfo.Real:
                    State.LastMessage = "수정으로 확인했습니다. 현재 약실은 실탄입니다.";
                    break;
                case ShotInfo.Fake:
                    State.LastMessage =  "수정으로 확인했습니다.현재 약실은 가짜탄입니다.";
                    break;
            default:
                break;
            }
            return shot;
        }

        public bool ReverseCurrentBullet()
        {
            bool isReversed = State.Gun.ReverseCurrentShot();
            State.LastMessage = isReversed ? "반전 룬을 사용했습니다.현재 약실의 탄이 반전되었습니다." : "반전할 탄이 없습니다.";
            return isReversed;
        }

        public void EmpowerNextShot()
        {
            isNextShotEmpowered = true;
            State.LastMessage = "마법가루를 사용했습니다. 다음 적중 실탄 피해가 강화됩니다.";
        }

        public void BlockNextEnemyAttack()
        {
            isNextEnemyAttackBlocked = true;
            State.LastMessage = "방패를 사용했습니다. 다음 몬스터 공격을 막습니다.";
        }

        public void SkipNextEnemyAttack()
        {
            isNextEnemyAttackSkipped = true;
            State.LastMessage = "밧줄을 사용했습니다. 다음 몬스터 공격을 넘깁니다.";
        }

        private BattleResult PlayerShot()
        {
            if (!State.Gun.CanFire)
            {
                State.Gun = CreateGun();
                State.Phase = BattlePhase.WaitingForPlayer;
                State.LastMessage = "모든 탄을 소비했습니다. 탄알집을 교체합니다.";
                return Result(State.LastMessage);
            }

            ShotInfo shot = State.Gun.GetNewShot();
            bool isTargetEnemy = State.SelectedTarget == BattleTarget.Enemy;

            if (shot == ShotInfo.Real)
            {
                Character target = isTargetEnemy ? State.Enemy : State.Player;
                int damage = isTargetEnemy && isNextShotEmpowered ? 2 : 1;
                target.GetStatData().GetBattleStat().GetDamage(damage);

                if (isTargetEnemy)
                {
                    isNextShotEmpowered = false;
                }

                State.LastMessage = isTargetEnemy
                    ? $"몬스터에게 사격했습니다. 실탄입니다! {damage} 피해를 입히고 턴이 유지됩니다."
                    : "자신에게 사격했습니다. 실탄입니다! 몬스터가 공격합니다.";

                if (TryEndBattle(out string endMessage))
                {
                    State.LastMessage = endMessage;
                    return Result(endMessage);
                }

                State.Phase = isTargetEnemy ? BattlePhase.WaitingForPlayer : BattlePhase.EnemyAttack;
                return Result(State.LastMessage);
            }

            if (shot == ShotInfo.Fake)
            {
                State.LastMessage = isTargetEnemy ? "몬스터에게 사격했습니다. 가짜탄입니다! 몬스터가 공격합니다." : "자신에게 사격했습니다. 가짜탄입니다! 턴이 유지됩니다.";
                State.Phase = isTargetEnemy ? BattlePhase.EnemyAttack : BattlePhase.WaitingForPlayer;
                return Result(State.LastMessage);
            }

            State.Gun = CreateGun();
            State.Phase = BattlePhase.WaitingForPlayer;
            State.LastMessage = "탄이 발사되지 않았습니다. 새 탄창을 삽입합니다.";
            return Result(State.LastMessage);
        }

        private BattleResult EnemyAttack()
        {
            if (isNextEnemyAttackSkipped)
            {
                isNextEnemyAttackSkipped = false;
                State.Phase = BattlePhase.WaitingForPlayer;
                State.LastMessage = $"{GetName(State.Enemy)}의 공격을 밧줄로 넘겼습니다.";
                return Result(State.LastMessage);
            }

            if (isNextEnemyAttackBlocked)
            {
                isNextEnemyAttackBlocked = false;
                State.LastMessage = $"{GetName(State.Enemy)}의 공격을 방패로 막았습니다.";
                State.Phase = BattlePhase.WaitingForPlayer;
                return Result(State.LastMessage);
            }

            State.Player.GetStatData().GetBattleStat().GetDamage(((EnemyStat)State.Enemy.GetStatData()).Atk);
            State.LastMessage = $"{GetName(State.Enemy)}이(가) 당신을 공격했습니다!";

            if (TryEndBattle(out string endMessage))
            {
                State.LastMessage = endMessage;
                return Result(endMessage);
            }

            State.Phase = BattlePhase.WaitingForPlayer;
            return Result(State.LastMessage);
        }

        private bool TryEndBattle(out string message)
        {
            if (!State.Player.GetStatData().GetBattleStat().IsAlive())
            {
                State.Phase = BattlePhase.Ended;
                message = "플레이어가 사망했습니다.";
                return true;
            }

            if (!State.Enemy.GetStatData().GetBattleStat().IsAlive())
            {
                State.Phase = BattlePhase.Ended;

                int money = ((EnemyStat)State.Enemy.GetStatData()).MoneyDrop;
                manager.Save.Player.Money += money;
                message = $"적이 사망했습니다. {money} 골드를 획득했습니다.";
                return true;
            }

            message = "";
            return false;
        }

        private BattleResult Result(string message)
        {
            bool isBattleOver = State.Phase == BattlePhase.Ended;
            return new BattleResult(State.Phase, State.Phase == BattlePhase.WaitingForPlayer, isBattleOver, message);
        }

        private static GunInfo CreateGun()
        {
            return GunInfo.GetRandomBattleInfo(DefaultShotCount);
        }

        private static string GetName(Character character)
        {
            return character.GetStatData().GetBattleStat().Name;
        }
    }
}
