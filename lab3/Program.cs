using lab3.Services;
using lab3.Interfaces;

namespace lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string txtPath = "textGame.txt";
            string jsonPath = "result.json";

            var converter = new TxtToGameConverter();
            converter.ConvertAndSave(txtPath, jsonPath, "Игра");
            IGameUI ui = new ConsoleGameUI();
            IGameLoader loader = new JsonGameLoader(jsonPath);

            var engine = new GameEngine(ui, loader);

            engine.Run();

            Console.ReadKey();
        }
    }
}