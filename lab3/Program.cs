using lab3.Services;
using lab3.Interfaces;
using lab3.Core;

namespace lab3
{
    internal class Program
    {


        static void Main(string[] args)
        {
            string txtPath = @"C:\Users\vlad4\OneDrive\Рабочий стол\textGame.txt";
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