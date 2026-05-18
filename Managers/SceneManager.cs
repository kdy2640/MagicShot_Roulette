using MagicShot_Roulette.Scenes;
using MagicShot_Roulette.Scenes.Main;
using MagicShot_Roulette.Scenes.Map;
using MagicShot_Roulette.Utils; 

namespace MagicShot_Roulette.Managers
{
    public class SceneManager
    {
        BaseScene nowScene;
        public SceneManager(BaseScene initial)
        {
            if (initial == null) nowScene = new MainScene();
            else nowScene = initial;
            nowScene.OnEnable();
        }
        public void Display()
        {
            Console.Clear();
            DisplayHelper.DisplayTitle(nowScene.GetType());
            nowScene.Display();
        }

        public void Update(KeyInfo key)
        {
            nowScene.Update(key);
        }

        public void ChangeScene(SceneType sceneType)
        {
            nowScene.OnDisable();
            nowScene = GetSceneInstance(sceneType);
            nowScene.OnEnable();
        }
        public BaseScene GetSceneInstance(SceneType sceneType)
        {
            BaseScene scene = null;
            switch (sceneType)
            {
                case SceneType.Main:
                    scene = new MainScene();
                    break;
                case SceneType.Help:
                    scene = new HelpScene();
                    break;
                case SceneType.Select:
                    scene = new SelectScene();
                    break; 
                case SceneType.Map:
                    scene = new MapScene();
                    break;
                case SceneType.Event:
                    scene = new EventScene();
                    break;
                case SceneType.Rest:
                    scene = new RestScene();
                    break;
                case SceneType.Battle:
                    scene = new BattleScene();
                    break;
                case SceneType.Boss:
                    scene = new BossScene();
                    break;
            }
            return scene;
        }
    }
}
