using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShot_Roulette.Datas.Battle
{
    public class Inventory
    {
        private List<Item> bags = new List<Item>();
        public int Capacity { get; set; }
        
        public Inventory() { Clear(); }

        public void Clear()
        {
            Capacity = 8;
            bags.Clear();
            bags.Add(new Potion());
            bags.Add(new Potion());
        }
        public int GetCount()
        {
            return bags.Count;
        }
        public Item GetIndex(int index)
        {
            return bags[index];
        }
        public void Add(Item item)
        {
            if (bags.Count == Capacity) return;
            bags.Add(item);
        }
        public void Remove(Item item)
        {
            int index = bags.FindIndex(x => x.Name == item.Name);
            if (index == -1) return;
            bags.RemoveAt(index);
        }

    }
}
