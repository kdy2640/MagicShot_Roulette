using MagicShot_Roulette.Managers;
using MagicShot_Roulette.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShot_Roulette.Scenes.Main
{
    public class MainScene : BaseScene
    {
        SelectVisualizer select;
        GameManager manager;
        public MainScene() : base(SceneType.Main)
        {
            manager = GameManager.GetInstance();
            select = new SelectVisualizer();
            select.AddData("새로운 시작"); 
            select.AddData("도움말");
            select.AddData("나가기"); 

        }
        public override void Display()
        {
            select.Visualize();
        }

        public override void Update(KeyInfo key)
        { 
            switch(key)
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
            switch(index)
            {
                case 0:
                    manager.Scene.ChangeScene(SceneType.Select);
                    break; 
                case 1:
                    manager.Scene.ChangeScene(SceneType.Help);
                    break;
                case 2:
                    Environment.Exit(0);
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
