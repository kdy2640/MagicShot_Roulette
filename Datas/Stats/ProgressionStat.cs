using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShot_Roulette.Datas.Stats
{
    public class ProgressionStat
    {
        private int floorIndex = 0;
        public int FloorIndex { get { return floorIndex; } set { floorIndex = value; ColIndex = 0; RowIndex = 1; } }
        public int ColIndex { get; set; }
        public int RowIndex { get; set; }
        public ProgressionStat()
        {
            FloorIndex = 0;
            ColIndex = 0;
            RowIndex = 1;
        }
        public void NextFloor()
        {
            FloorIndex++;
            ColIndex = 0;
            RowIndex = 1;
        }
    }
}
