using System.Reflection.Metadata;

namespace HeroQuest
{
    public class Program
    {
        public static void Main(string[] args) // Corrected method name to "Main" (C# is case-sensitive)
        {
            // Create an instance of the GameManager and start the game.
            GameManager gameManager = new GameManager();
            gameManager.StartGame();
        }
    }
}
