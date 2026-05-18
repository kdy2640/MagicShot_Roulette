using MagicShot_Roulette.Datas.Stats;
using MagicShot_Roulette.Managers;

namespace MagicShot_Roulette.Datas.Battle
{
    public abstract class Enemy : Character
    {
        protected Enemy(CharacterStat stat) : base(stat)
        {
        }

        public static Enemy GetEnemy(bool isBoss)
        {
            Random rand = new Random();
            GameManager manager = GameManager.GetInstance();
            int floorIndex = manager.Save.Player.GetProgressionData().FloorIndex;
            if(floorIndex == 0)
            {
                if(isBoss)
                { 
                    return GetEnemy(EnemyType.Troll);
                }
                else
                {
                    int index = rand.Next(0, (int)EnemyType.Wolf + 1);
                    return GetEnemy((EnemyType)index);
                }
            }
            else
            {
                if(isBoss)
                {
                    return GetEnemy(EnemyType.Golem);
                }
                else
                {
                    int index = rand.Next((int)EnemyType.Wolf, (int)EnemyType.Spider + 1);
                    return GetEnemy((EnemyType)index);
                }
            }
        }
        public static Enemy GetEnemy(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.Slime:
                    return new Slime();
                case EnemyType.Goblin:
                    return new Goblin();
                case EnemyType.Wolf:
                    return new Wolf();
                case EnemyType.Skeleton:
                    return new Skeleton();
                case EnemyType.Orc:
                    return new Orc();
                case EnemyType.Spider:
                    return new Spider();
                case EnemyType.Troll:
                    return new Troll();
                case EnemyType.Golem:
                    return new Golem();
                default:
                    return new Slime();
            }
        }
    }

    public class Slime : Enemy
    {
        public Slime() : base(new EnemyStat(EnemyType.Slime))
        {
        }
    }

    public class Goblin : Enemy
    {
        public Goblin() : base(new EnemyStat(EnemyType.Goblin))
        {
        }
    }

    public class Wolf : Enemy
    {
        public Wolf() : base(new EnemyStat(EnemyType.Wolf))
        {
        }
    }

    public class Skeleton : Enemy
    {
        public Skeleton() : base(new EnemyStat(EnemyType.Skeleton))
        {
        }
    }

    public class Orc : Enemy
    {
        public Orc() : base(new EnemyStat(EnemyType.Orc))
        {
        }
    }

    public class Spider : Enemy
    {
        public Spider() : base(new EnemyStat(EnemyType.Spider))
        {
        }
    }

    public class Troll : Enemy
    {
        public Troll() : base(new EnemyStat(EnemyType.Troll))
        {
        }
    }

    public class Golem : Enemy
    {
        public Golem() : base(new EnemyStat(EnemyType.Golem))
        {
        }
    }
}
