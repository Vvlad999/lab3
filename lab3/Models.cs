using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Models
{
    public class Game
    {
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }
        public int _startScene;
        public int StartScene { get { return _startScene; } set { _startScene = value; } }
        public List<Scene> Scenes { get; set; }
    }

    public class Scene
    {
        private int _No;
        public int No { get { return _No; } set { _No = value; } }
        private string _text;
        public string Text { get { return _text; } set { _text = value; } }
        public List<Answer> Answers { get; set; }
    }
    public class Answer
    {
        private string _text;
        public string Text { get { return _text; } set { _text = value; } }
        private int _nextScene;
        public int NextScene { get { return _nextScene; } set { _nextScene = value; } }
    }


}