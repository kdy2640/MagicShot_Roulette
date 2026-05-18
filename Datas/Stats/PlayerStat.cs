using MagicShot_Roulette.Datas.Battle;

namespace MagicShot_Roulette.Datas.Stats
{
    public enum ClassType
    {
        Knight = 0,
        Magician = 1
    }

    public enum PlayerUpgradeType
    {
        HP,
        MP,
        MaxItem
    }

    public class PlayerStat : CharacterStat
    {
        private static readonly Random random = new Random();
        public readonly string KnightSkillDescription = "다음 턴 피격 1회 방어";
        public readonly string MagicianSkillDescription = "현재 약실을 반전";
        public static readonly (int, int, int)[] DefaultStat = new (int, int, int)[2] { (6, 3, 4), (5, 6, 4) };
        private ClassType classType;
        public ClassType Class { get { return classType; } set { classType = value;  InitializeClass(value); } }
        ProgressionStat progression;
        public int Money { get; set; } = 5;
        public PlayerStat()
        { 
            progression = new ProgressionStat();
            IsPlayer = true;
        }
        
        public ProgressionStat GetProgressionData()
        {
            return progression;
        }
        public void InitializeClass(ClassType type)
        {
            BattleStat stat = GetBattleStat();
            (int hp, int mp, int itemCapacity) = DefaultStat[(int)type];
            stat.HP = hp;
            stat.MP = mp;
            stat.ItemCapacity = itemCapacity;
            stat.MaxHP = hp;
            stat.MaxMP = mp;
            stat.MaxItemCapacity = itemCapacity;
            GetInven().Capacity = itemCapacity;

            if (type == ClassType.Knight)
            { 
                stat.SkillDescription = KnightSkillDescription; 
            }
            else if(type == ClassType.Magician)
            {  
                stat.SkillDescription = MagicianSkillDescription;
            }
        }

        public string UpgradeStat(PlayerUpgradeType upgrade)
        {
            BattleStat stat = GetBattleStat();
            Inventory inven = GetInven();

            switch (upgrade)
            {
                case PlayerUpgradeType.HP:
                    stat.MaxHP++;
                    stat.HP++;
                    return "최대 HP가 1 증가했습니다.";
                case PlayerUpgradeType.MP:
                    stat.MaxMP++;
                    stat.MP++;
                    return "최대 MP가 1 증가했습니다.";
                case PlayerUpgradeType.MaxItem:
                    stat.MaxItemCapacity++;
                    stat.ItemCapacity = stat.MaxItemCapacity;
                    inven.Capacity = stat.MaxItemCapacity;
                    return "인벤토리 크기가 1 증가했습니다.";
                default:
                    return "";
            }
        }

        public string UpgradeRandomStat()
        {
            PlayerUpgradeType upgrade = (PlayerUpgradeType)random.Next(0, 3);
            return UpgradeStat(upgrade);
        }

        public void RecoverAll()
        {
            BattleStat stat = GetBattleStat();
            stat.HP = stat.MaxHP;
            stat.MP = stat.MaxMP;
        }
    }
}
