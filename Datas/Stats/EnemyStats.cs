using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShot_Roulette.Datas.Stats
{
    public enum EnemyType
    {
        Slime = 0,
        Goblin = 1,
        Wolf = 2,
        Skeleton = 3,
        Orc = 4, 
        Spider = 5,
        Troll = 6,
        Golem = 7
    }
    public class EnemyStat: CharacterStat
    {
        private EnemyType enemyType;
        public EnemyType Type { get { return enemyType; } set { enemyType = value; InitializeType(value); } }

        public int MoneyDrop { get; set; }
        public int Atk { get; set; }
        public EnemyStat(EnemyType type)
        {
            this.enemyType = type;
            IsPlayer = false;
            InitializeType(type);
        }
        private void InitializeType(EnemyType type)
        {
            BattleStat stat = GetBattleStat();
            stat.MP = 0;
            stat.ItemCapacity = 0;
            stat.MaxMP = 0;
            stat.MaxItemCapacity = 0;

            switch (type)
            {
                case EnemyType.Slime:
                    stat.Name = "슬라임";
                    stat.HP = 2;
                    MoneyDrop = 2;
                    Atk = 1;
                    break;
                case EnemyType.Goblin:
                    stat.Name = "고블린";
                    stat.HP = 2;  
                    MoneyDrop = 4;
                    Atk = 1;
                    break;
                case EnemyType.Wolf:
                    stat.Name = "늑대";
                    stat.HP = 3;
                    MoneyDrop = 5;
                    Atk = 1;
                    break;
                case EnemyType.Skeleton:
                    stat.Name = "스켈레톤";
                    stat.HP = 2;
                    MoneyDrop = 6;
                    Atk = 2;
                    break;
                case EnemyType.Orc:
                    stat.Name = "오크";
                    stat.HP = 5;
                    MoneyDrop = 8;
                    Atk = 2;
                    break;
                case EnemyType.Spider:
                    stat.Name = "거미";
                    stat.HP = 4;
                    MoneyDrop = 5;
                    Atk = 3;
                    break;
                case EnemyType.Troll:
                    stat.Name = "트롤";
                    stat.HP = 6;
                    MoneyDrop = 10;
                    Atk = 3;
                    break;
                case EnemyType.Golem:
                    stat.Name = "골렘";
                    stat.HP = 10;
                    MoneyDrop = 14;
                    Atk = 4;
                    break;
            }

            stat.MaxHP = stat.HP;
        }
    }
}
