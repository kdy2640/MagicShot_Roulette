using MagicShot_Roulette.Datas.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShot_Roulette.Map
{
    public class MapHandler
    {
        public Floor NowFloor { get; set; }
        private FloorVisualizer visualizer;
        public MapHandler() 
        {
            NowFloor = new Floor();
            visualizer = new FloorVisualizer();
        } 
        public void SetMapData(ProgressionStat progression)
        {
            if(progression.FloorIndex == 0)
            {
                NowFloor.SetFirstFloor();
            }
            else if(progression.FloorIndex == 1)
            {
                NowFloor.SetLastFloor();
            }
            else
            {
                Console.WriteLine("플레이해주셔서 감사합니다.");
                Environment.Exit(0);
            }

            NowFloor.SetPlayerPosition(progression.RowIndex, progression.ColIndex);
            visualizer.Initiazlie(NowFloor);
        }
        public void DisplayFloor()
        {
            visualizer.Visualizer(NowFloor); 
        }
    }
}
