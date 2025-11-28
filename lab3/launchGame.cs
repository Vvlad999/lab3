using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace lab3
{
    internal class Launch
    {
        private string _path = "result.json";
        public string Path {  get { return _path; } set { _path = value; } }

        public Launch()
        {
           
            string  text = File.ReadAllText(Path);
            Game game = JsonSerializer.Deserialize<Game>(text);
            Scene currentScene = game.Scenes.Where( c=>c.No==game.StartScene).FirstOrDefault();
            int i = 0;
            do
            {
                Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                for (int j = 0; j < currentScene.Text.Length; j += 100)
                {
                    int length = Math.Min(100, currentScene.Text.Length - j);
                    string line = currentScene.Text.Substring(j, length);
                    Console.WriteLine($"║ {line.PadRight(100)} ║");
                }
                Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════════════════════════╝");
                foreach (var answer in currentScene.Answers)
                {
                    Console.WriteLine(currentScene.Answers.IndexOf(answer) + ") " + answer.Text);
                }
                i++;
                int ans = readInt(currentScene);
                currentScene = game.Scenes.Where(c=>c.No == currentScene.Answers.ElementAt(ans).NextScene).FirstOrDefault();
            } while (currentScene.Answers.Count!=0);

            

        }
        int readInt(Scene c)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int value) && value >= 0 && value < c.Answers.Count) return value;
                Console.WriteLine("неверный ввод");
            }
        }

    }
}
