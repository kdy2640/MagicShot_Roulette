using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MagicShot_Roulette.Datas.Stats
{
    public class BattleStat
    {
        public int HP { get; set; }
        public int MP { get; set; }
        public int Level { get; set; }
        public int ItemCapacity { get; set; }
        public int MaxHP { get; set; }
        public int MaxMP { get; set; }
        public int MaxItemCapacity { get; set; }
        public string Name { get; set; }
        public string SkillDescription { get; set; }

        public bool GetDamage(int damage)
        {
            HP = Math.Max(0, HP - damage);
            return HP > 0;
        }
        public bool IsAlive() {return HP > 0;}
    }
}
