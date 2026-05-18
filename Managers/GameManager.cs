using MagicShot_Roulette.Datas.Battle;
using MagicShot_Roulette.Scenes;
using MagicShot_Roulette.Scenes.Main;

namespace MagicShot_Roulette.Managers
{
    class GameManager
    {
        private static GameManager instance;
        public SceneManager Scene { get; }
        public InputManager Input{ get; }
        public SaveManager Save { get; }
        public BattleManager Battle { get; }

        private GameManager()
        {
            if(instance == null)
            {
                instance = this;
            }
            Input = new InputManager();
            Save = new SaveManager();
            Battle = new BattleManager(Save.Player);
            Scene = new SceneManager(new MainScene());
        } 
        public static GameManager GetInstance()
        {
            if(instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
        
        public void Run()
        { 
            while(true)
            {
                Scene.Display();
                KeyInfo key = KeyInfo.Esc;
                InputResponse response = Input.GetKeyInfo(out key);
                switch (response)
                {
                    case InputResponse.Success:
                        Scene.Update(key);
                        break;
                    case InputResponse.DelayContinue:
                        Thread.Sleep(1000);
                        continue;
                    case InputResponse.DelaySuccess:
                        Scene.Update(key);
                        Thread.Sleep(2000);
                        continue;
                    case InputResponse.Exit:
                        ExitGame();
                        break; 
                }
            }
        }
        
        private void ExitGame()
        {
            Environment.Exit(0);
        }
    }
}
