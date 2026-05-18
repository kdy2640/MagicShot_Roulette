using MagicShot_Roulette.Datas.Battle;
using MagicShot_Roulette.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShot_Roulette.Datas.Stats
{
    public class CharacterStat
    {
        private BattleStat battle;
        private Inventory inven;
        protected bool IsPlayer;

        public CharacterStat() 
        {
            battle = new BattleStat();
            inven = new Inventory();
        }
        public BattleStat GetBattleStat()
        {
            return battle;
        }
        public Inventory GetInven()
        {
            return inven;
        }
    }
}
