using MagicShot_Roulette.Managers;

namespace MagicShot_Roulette
{
    public class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            GameManager manager = GameManager.GetInstance();
            manager.Run();
        }
    }

}