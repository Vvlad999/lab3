using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Models
{
    public class Game
    {
        public string Name { get; set; }
        public int StartSceneId { get; set; }
        public List<Scene> Scenes { get; set; } = new List<Scene>();
    }

    public class Scene
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }

    public class Answer
    {
        public string Text { get; set; }
        public int NextSceneId { get; set; }
    }
}