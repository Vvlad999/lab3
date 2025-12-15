using System;
using System.Text.Json;
using lab3.Interfaces;
using lab3.Models;

namespace lab3.Services
{
    public class ConsoleGameUI : IGameUI
    {
        public void ShowScene(Scene scene)
        {
            Console.Clear();
            Console.WriteLine("╔" + new string('═', 100) + "╗");

            // Логика разбиения текста на строки
            for (int j = 0; j < scene.Text.Length; j += 100)
            {
                int length = Math.Min(100, scene.Text.Length - j);
                string line = scene.Text.Substring(j, length);
                Console.WriteLine($"║ {line.PadRight(100)} ║");
            }

            Console.WriteLine("╚" + new string('═', 100) + "╝");

            Console.WriteLine("\nВарианты действий:");
            for (int i = 0; i < scene.Answers.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {scene.Answers[i].Text}");
            }
        }

        public void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public int GetUserChoice(int maxOption)
        {
            Console.Write("\nВаш выбор > ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int value) && value > 0 && value <= maxOption)
                {
                    return value - 1;
                }
                ShowError("Неверный ввод. Попробуйте еще раз: ");
            }
        }
    }

    // чтение JSON файла
    public class JsonGameLoader : IGameLoader
    {
        private readonly string _filePath;

        public JsonGameLoader(string filePath)
        {
            _filePath = filePath;
        }

        public Game Load()
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException($"Файл {_filePath} не найден.");

            string text = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<Game>(text);
        }
    }

    // конвертация
    public class TxtToGameConverter
    {
        public void ConvertAndSave(string txtPath, string jsonPath, string gameName)
        {

            string[] lines = File.ReadAllLines(txtPath);
            var game = new Game { Name = gameName, StartSceneId = 1 };

            Scene currentScene = null;

            foreach (var line in lines)
            {
                string trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmed)) continue;

                if (trimmed.StartsWith("СЦЕНА"))
                {
                    currentScene = new Scene();

                    string digits = new string(trimmed.Where(char.IsDigit).ToArray());
                    currentScene.Id = int.Parse(digits);
                    game.Scenes.Add(currentScene);
                }
                else if (currentScene != null)
                {
                    if (trimmed.StartsWith("#"))
                    {
                        var parts = trimmed.Split('→');
                        if (parts.Length == 2)
                        {
                            var answer = new Answer
                            {
                                Text = parts[0].Trim('#', ' '),
                                NextSceneId = int.Parse(new string(parts[1].Where(char.IsDigit).ToArray()))
                            };
                            currentScene.Answers.Add(answer);
                        }
                    }
                    else
                    {
                        currentScene.Text += trimmed + " ";
                    }
                }
            }

            var options = new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            File.WriteAllText(jsonPath, JsonSerializer.Serialize(game, options));
        }
    }
}