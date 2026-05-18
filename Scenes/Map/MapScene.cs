using MagicShot_Roulette.Managers;
using MagicShot_Roulette.Utils;
using MagicShot_Roulette.Map;
using MagicShot_Roulette.Datas.Stats;

namespace MagicShot_Roulette.Scenes.Map
{
    public class MapScene : BaseScene
    {
        GameManager manager;
        MapHandler map;
        SelectVisualizer select;
        public MapScene() : base(SceneType.Map)
        {
            manager = GameManager.GetInstance();
            map = new MapHandler();
            select = new SelectVisualizer();

            ProgressionStat stat = manager.Save.Player.GetProgressionData();
            map.SetMapData(stat);
            List<(int, int)> nextNodes = map.NowFloor.GetNode(stat.RowIndex, stat.ColIndex).GetNextNodes();

            for (int i = 0; i < nextNodes.Count; i++)
            {
                Node nextNode = map.NowFloor.GetNode(stat.RowIndex + nextNodes[i].Item1, stat.ColIndex + nextNodes[i].Item2);
                string temp = FloorVisualizer.GetNodeString(nextNode.NodeType);
                select.AddData($"{temp}");
            }
        }

        public override void Display()
        {
            ProgressionStat stat = manager.Save.Player.GetProgressionData();
            if(stat.FloorIndex == 2)
            {
                Console.Clear();
                Console.WriteLine("현재 개발단계는 여기까지입니다.");
                Console.WriteLine("플레이해주셔서 감사합니다.");
                Environment.Exit(0);
            }
            map.SetMapData(stat);
            map.DisplayFloor();
            Console.WriteLine(DisplayHelper.DoubleLine);
            DisplayHelper.DisplayPlayerInfo(manager.Save.Player,true);
            Console.WriteLine(DisplayHelper.DoubleLine);

            select.Visualize(); 
        }

        public override void Update(KeyInfo key)
        {

            switch (key)
            {
                case KeyInfo.Up:
                    select.Visualize();
                    select.PrevSelect(); 
                    break;
                case KeyInfo.Down:
                    select.Visualize();
                    select.NextSelect(); 
                    break;
                case KeyInfo.Space:
                    SelectHandle(select.GetSelect()); 
                    break;
            }
        }

        public void SelectHandle(int index)
        {

            if (map.NowFloor.ColumnCount - 1 == manager.Save.Player.GetProgressionData().ColIndex)
            {
                manager.Save.Player.GetProgressionData().NextFloor();
            }
            ProgressionStat stat = manager.Save.Player.GetProgressionData();
            Node nowNode = map.NowFloor.GetNode(stat.RowIndex, stat.ColIndex);
            List<(int, int)> nowNodeIndex = nowNode.GetNextNodes();
            if(index < nowNodeIndex.Count)
            { 
                stat.ColIndex = stat.ColIndex + nowNodeIndex[index].Item2;
                stat.RowIndex = stat.RowIndex + nowNodeIndex[index].Item1;
            }

            Node nextNode = map.NowFloor.GetNode(stat.RowIndex, stat.ColIndex);
            switch (nextNode.NodeType)
            {  
                case NodeType.Battle:
                    manager.Scene.ChangeScene(SceneType.Battle);
                    break;
                case NodeType.Event:
                    manager.Scene.ChangeScene(SceneType.Event);
                    break;
                case NodeType.Rest:
                    manager.Scene.ChangeScene(SceneType.Rest);
                    break;
                case NodeType.Boss:
                    manager.Scene.ChangeScene(SceneType.Boss);
                    break;
            } 
        }
        public override void OnDisable()
        {  
        }

        public override void OnEnable()
        { 
        }

    }
}
