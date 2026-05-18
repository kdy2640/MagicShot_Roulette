using MagicShot_Roulette.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MagicShot_Roulette.Managers
{
    public enum KeyInfo
    {
        Left = 1,
        Right = 2,
        Up = 3,
        Down = 4,
        Space = 5,
        Cancle = 6,
        Esc = 7,
        Count,
        False
    }
    public enum InputResponse
    {
        Success = 0,
        DelayContinue = 1,
        DelaySuccess = 2,
        Exit = 3,
    }
    public class InputManager
    {
        public InputManager()
        {

        }
        public bool IsFalseInput { get; set; } = false;
        public InputResponse GetKeyInfo(out KeyInfo key)
        {
            if(IsFalseInput)
            {
                key = KeyInfo.False;
                return InputResponse.DelaySuccess;
            }
            DisplayHelper.DisplayInput();
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    key = KeyInfo.Left;
                    break;
                case ConsoleKey.RightArrow:
                    key = KeyInfo.Right;
                    break;
                case ConsoleKey.UpArrow:
                    key = KeyInfo.Up;
                    break;
                case ConsoleKey.DownArrow:
                    key = KeyInfo.Down;
                    break;
                case ConsoleKey.Spacebar:
                    key = KeyInfo.Space;
                    break;
                case ConsoleKey.C:
                    key = KeyInfo.Cancle;
                    break;
                case ConsoleKey.Escape:
                    key = KeyInfo.Esc;
                    return InputResponse.Exit;
                default:
                    key = KeyInfo.Esc;
                    Console.WriteLine("잘못된 입력입니다.");
                    return InputResponse.DelayContinue;
            } 
            return InputResponse.Success;
        }
    }
}
