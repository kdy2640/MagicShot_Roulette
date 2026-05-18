using MagicShot_Roulette.Datas.Battle;
using MagicShot_Roulette.Managers;
using MagicShot_Roulette.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShot_Roulette.Scenes.Map
{ 
    public class EventScene : BaseScene
    {
        SelectVisualizer select;
        GameManager manager;
        Item nowItem;
        public EventScene() : base(SceneType.Event)
        {
            manager = GameManager.GetInstance();
            Random rand = new Random();
            nowItem = Item.Create((ItemType)rand.Next(1, (int)ItemType.Count - 2));
            select = new SelectVisualizer();
            select.AddData("아이템을 줍는다.");
            select.AddData("지나친다.");
        }

        public override void Display()
        {
            Console.WriteLine($"{nowItem.Name}을 발견했다!");
            Console.WriteLine(DisplayHelper.DoubleLine); 
            select.Visualize();
        } 
        public override void OnDisable()
        { 

        }

        public override void OnEnable()
        { 

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
                    SelectHandler(select.GetSelect());
                    break;
            }
        }

        public void SelectHandler(int index)
        {
            switch (index)
            {
                case 0:
                    manager.Save.Player.GetInven().Add(nowItem);
                    break;
                case 1:
                    break;
            }
            manager.Scene.ChangeScene(SceneType.Map);
        }
    }
}
