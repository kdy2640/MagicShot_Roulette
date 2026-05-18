using MagicShot_Roulette.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MagicShot_Roulette.Scenes
{
    public enum SceneType
    {
        Main,
        Help,
        Select,
        Map,
        Event,
        Rest,
        Battle,
        Boss
    }
    public abstract class BaseScene
    {
        private SceneType type; 
        public BaseScene(SceneType type) { this.type = type; }
        public SceneType GetType() { return type; }
        public abstract void Display();
        public abstract void Update(KeyInfo key);
        public abstract void OnEnable();
        public abstract void OnDisable(); 
    }
}
