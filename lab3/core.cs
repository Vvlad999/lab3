using lab3.Interfaces;
using lab3.Models;

namespace lab3.Core
{
    public class GameEngine
    {
        private readonly IGameUI _ui;
        private readonly IGameLoader _loader;
        private Game _game;

        public GameEngine(IGameUI ui, IGameLoader loader)
        {
            _ui = ui;
            _loader = loader;
        }

        public void Run()
        {
           
            try
            {
                _game = _loader.Load();
            }
            catch (Exception ex)
            {
                _ui.ShowError("Ошибка загрузки игры: " + ex.Message);
                return;
            }

            // 2. Игровой цикл
            Scene currentScene = _game.Scenes.FirstOrDefault(s => s.Id == _game.StartSceneId);

            while (currentScene != null)
            {
                _ui.ShowScene(currentScene);

                if (currentScene.Answers.Count == 0)
                {
                    _ui.ShowError("\nКонец игры.");
                    break;
                }

                int choiceIndex = _ui.GetUserChoice(currentScene.Answers.Count);
                int nextSceneId = currentScene.Answers[choiceIndex].NextSceneId;

                currentScene = _game.Scenes.FirstOrDefault(s => s.Id == nextSceneId);
            }
        }
    }
}