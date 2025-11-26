using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace lab3
{
    internal class txt_tojson
    {
        private string _path = "C:\\Users\\vlad4\\OneDrive\\Рабочий стол\\textGame.txt";
        public string Path {  get { return _path; } set { _path = value;} }

       public void Serialization()
        {
            string[] text =  File.ReadAllLines(Path);
            int i = 0;
            var game = new Game();
            game.Scenes = new List<Scene>();
            game.StartScene = 1;
            game.Name = "Someone";
            Scene currentscene = null;
            while (text.Length > i) {
                string line = text[i].Trim();
                if (line.StartsWith("СЦЕНА"))
                {
                    currentscene = new Scene();
                    currentscene.Answers = new List<Answer>();
                    currentscene.No = int.Parse(string.Join("", line.Where(char.IsDigit).ToArray()));
                    game.Scenes.Add(currentscene);
                }
                else if (currentscene != null) { 
                    if(line.StartsWith("#"))
                    {
                        Answer answer = new Answer();
                        answer.Text = line.Substring(1,line.IndexOf("→"));
                        answer.NextScene = int.Parse(string.Join("", line.Substring(line.IndexOf("→")).Where(char.IsDigit).ToArray()));
                        currentscene.Answers.Add(answer);
                    }
                    else
                    {
                        currentscene.Text = currentscene.Text + line;
                    }
                    
                }
                i++;
            }
            var options = new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

            string jsonResult = JsonSerializer.Serialize(game,options);

            File.WriteAllText("result.json", jsonResult);
        }

    }
}
