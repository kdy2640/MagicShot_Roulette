using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShot_Roulette.Datas.Battle
{
    public enum ItemType
    {
        None,
        Rope,
        Wine,
        Quartz,
        Potion,
        MagicPowder, 
        Shield,
        ReverseRune,
        Count,
    }
    public class Item
    {
        public string Name { get; protected set; }
        
        public string Description { get; protected set; }
        public ItemType type { get; protected set; }
        protected Item(string name, string desc)
        {
            Name = name;
            Description = desc;
        }

        public Item(Item activeItem)
        { 
            this.type =  activeItem.type;
            this.Description = activeItem.Description;
            this.Name = activeItem.Name;

        }
        public static Item Create(ItemType item) 
        {
            switch (item)
            {
                case ItemType.None:
                    return new NoneItem();
                case ItemType.Rope:
                    return new Rope();
                case ItemType.Wine:
                    return new Wine();
                case ItemType.Quartz:
                    return new Quartz();
                case ItemType.Potion:
                    return new Potion();
                case ItemType.MagicPowder:
                    return new MagicPowder();
                case ItemType.Shield:
                    return new Shield();
                case ItemType.ReverseRune:
                    return new ReverseRune();
                default:
                    return new NoneItem();
            }
        }
    }

    public class NoneItem : Item
    {
        public NoneItem() : base("X", "X") { type = ItemType.None; }
    }
    public class Rope : Item
    {
        public Rope() : base("밧줄", "상대의 턴을 1 턴 넘깁니다."){ type = ItemType.Rope; }
    }

    public class Wine : Item
    {
        public Wine() : base("와인", "현재 약실을 제거합니다.") { type = ItemType.Wine;  }
    }

    public class Quartz : Item
    {
        public Quartz() : base("수정", "현제 약실을 확인합니다.") { type = ItemType.Quartz; }
    }

    public class Potion : Item
    {
        public Potion() : base("포션", "HP를 1 회복합니다.") { type = ItemType.Potion; }
    }

    public class MagicPowder : Item
    {
        public MagicPowder() : base("마법가루", "현재 약실이 실탄일 경우, 데미지를 2배로 합니다.") { type = ItemType.MagicPowder; }
    }

    public class Shield : Item
    {
        public Shield() : base("방패", "다음 턴 피격될 시 데미지를 무효화합니다.") { type = ItemType.Shield; }
    }


    public class ReverseRune : Item
    {
        public ReverseRune() : base("반전 룬", "현재 약실의 탄을 반전합니다.") { type = ItemType.ReverseRune; }
    }
}
