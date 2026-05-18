using MagicShot_Roulette.Datas.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShot_Roulette.Managers
{
    public class SaveManager
    {
        public PlayerStat Player { get; set; }
        public SaveManager() { Player = new PlayerStat(); }

    }
}
