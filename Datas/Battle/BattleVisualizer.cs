using MagicShot_Roulette.Datas.Stats;
using MagicShot_Roulette.Utils;

namespace MagicShot_Roulette.Datas.Battle
{
    static class BattleVisualizer
    {
        public static bool IsGunReversed { get; set; }

        public static void Display(BattleState state)
        {
            if (state.Player is null || state.Enemy is null)
            {
                return;
            }

            DisplayBattleTable(state);
            Console.WriteLine(DisplayHelper.SingleLine);
            DisplayHelper.DisplayPlayerInfo((PlayerStat)state.Player.GetStatData(), false);
            DisplayHelper.DisplayEnemyInfo((EnemyStat)state.Enemy.GetStatData());
            Console.WriteLine(DisplayHelper.SingleLine);

            if (state.Gun is not null)
            {
                DisplayGun(state.Gun);
            }

            if (!string.IsNullOrWhiteSpace(state.LastMessage))
            {
                Console.WriteLine(state.LastMessage);
            }

            Console.WriteLine(DisplayHelper.DoubleLine);
        }

        private static void DisplayBattleTable(BattleState state)
        {
            string playerName = state.Player!.GetStatData().GetBattleStat().Name;
            string enemyName = state.Enemy!.GetStatData().GetBattleStat().Name;
            string aim = state.SelectedTarget == BattleTarget.Enemy
                ? $"{playerName}  [ ︻╦╤─ ]  {enemyName}"
                : $"{playerName}  [ ─╤╦︻ ]  {enemyName}";

            Console.WriteLine(aim);
            switch (state.Phase)
            {
                case BattlePhase.WaitingForPlayer:
                    Console.WriteLine($"행동을 입력하세요.");
                    break;
                case BattlePhase.PlayerShot:
                    Console.WriteLine($"플레이어 사격");
                    break;
                case BattlePhase.EnemyAttack:

                    Console.WriteLine($"몬스터 공격");
                    break;
                case BattlePhase.Ended:

                    Console.WriteLine($"전투 종료");
                    break;
                default:
                    break;
            }
        }

        private static void DisplayGun(IReadOnlyGunInfo gunInfo)
        {
            Console.Write("탄알: ");
            for (int i = 0; i < gunInfo.MaxShotCount; i++)
            {
                Console.Write(IsGunReversed ? "?" : i < gunInfo.NowShotCount ? "x" : "o");
            }

            if (IsGunReversed)
            {
                Console.WriteLine();
                return;
            }

            Console.Write(" | 실탄: ");
            for (int i = 0; i < gunInfo.RealShotCount; i++)
            {
                Console.Write("*");
            }

            Console.Write(" | 가짜탄: ");
            for (int i = 0; i < gunInfo.FakeShotCount; i++)
            {
                Console.Write("-");
            }

            Console.WriteLine();
        }
    }
}
