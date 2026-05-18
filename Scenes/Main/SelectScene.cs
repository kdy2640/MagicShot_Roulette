using MagicShot_Roulette.Managers;
using MagicShot_Roulette.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MagicShot_Roulette.Scenes.Main
{
    public class SelectScene : BaseScene
    {
        SelectVisualizer select;
        GameManager manager;
        public SelectScene() : base(SceneType.Select)
        {
            manager = GameManager.GetInstance();
            select = new SelectVisualizer();
            Console.WriteLine("클래스를 선택하세요.");
            select.AddData("검사 | HP: 4 | MP: 3 | 인벤토리 크기: 4 | 특수능력[실드]: 다음 턴 피격 1회 방어");
            select.AddData("마법사 | HP: 3 | MP : 4 | 인벤토리 크기: 4 | 특수능력[리버스]: 현재 약실을 반전");
        }

        public override void Display()
        {
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
                    {
                        int index = select.GetSelect();
                        switch (index)
                        {
                            case 0:
                                manager.Save.Player.Class = Datas.Stats.ClassType.Knight;
                                break;
                            case 1:
                                manager.Save.Player.Class = Datas.Stats.ClassType.Magician;
                                break;
                        }
                        GetUserName();
                        break;
                    }
            }
        }

        public void GetUserName()
        {
            Console.WriteLine("이름을 입력하세요.");
            string name = Console.ReadLine();
            manager.Save.Player.GetBattleStat().Name = name;
            manager.Scene.ChangeScene(SceneType.Map);
        }


        public override void OnDisable()
        {
        }

        public override void OnEnable()
        {
        }


    }
}
