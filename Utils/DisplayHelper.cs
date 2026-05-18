using MagicShot_Roulette.Datas.Battle;
using MagicShot_Roulette.Datas.Stats;
using MagicShot_Roulette.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShot_Roulette.Utils
{
    public static class DisplayHelper
    {
        public readonly static string SingleLine = "----------------";
        public readonly static string DoubleLine = "================";

        public static void DisplayTitle(SceneType type)
        {
            Console.WriteLine(DoubleLine);
            Console.WriteLine(GetTitleString(type));
            Console.WriteLine(DoubleLine);
        }

        private static string GetTitleString(SceneType type)
        {
            switch(type)
            {
                case SceneType.Main:
                    return "MagicShot Roulette - 메인 화면";
                case SceneType.Select:
                    return "MagicShot Roulette - 플레이어 설정";
                case SceneType.Help:
                    return "MagicShot Roulette - 게임 방법";
                case SceneType.Map:
                    return "MagicShot Roulette - 경로 선택";
                case SceneType.Event:
                    return "MagicShot Roulette - 이벤트";
                case SceneType.Rest:
                    return "MagicShot Roulette - 휴식";
                case SceneType.Battle:
                    return "MagicShot Roulette - 전투";
            }
            return "";
        }

        public static void DisplayInput()
        {
            Console.WriteLine(SingleLine);
            Console.WriteLine("방향키: 선택 | Space: 확인 | C: 취소 | ESC: 게임종료");
            Console.WriteLine(DoubleLine);
        }
        public static void DisplayPlayerInfo(PlayerStat data, bool isOpenInven) 
        {
            BattleStat stat = data.GetBattleStat();
            Console.WriteLine($"{stat.Name} | HP : {stat.HP}/{stat.MaxHP} | MP : {stat.MP}/{stat.MaxMP} | Money : {data.Money} | Skill : {stat.SkillDescription}");
            if (isOpenInven)
            {
                DisplayInventory(data.GetInven());
            } 
        }
        public static void DisplayPlayerInfo(PlayerStat data)
        {
            BattleStat stat = data.GetBattleStat();
            Console.WriteLine($"{stat.Name} | HP : {stat.HP}/{stat.MaxHP} | MP : {stat.MP}/{stat.MaxMP} | Money : {data.Money} | Skill : {stat.SkillDescription}");
            DisplayInventory(data.GetInven());
        }
        public static void DisplayEnemyInfo(EnemyStat data)
        {
            BattleStat stat = data.GetBattleStat();
            Console.WriteLine($"{stat.Name} | HP : {stat.HP} | ATK : {data.Atk}");
        }
        public static void DisplayInventory(Inventory inven)
        {
            Console.WriteLine($"Inventory : {inven.GetCount()}/{inven.Capacity}");
            if (inven.GetCount() == 0)
            {
                Console.WriteLine("비어있습니다.");
                return;
            }

            for (int i = 0; i < inven.GetCount(); i++)
            {
                Item item = inven.GetIndex(i);
                Console.WriteLine($"{i + 1}. {item.Name} | {item.Description}");
            }
        }
    }
}
