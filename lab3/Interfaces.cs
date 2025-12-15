using lab3.Models;

namespace lab3.Interfaces
{
    public interface IGameUI
    {
        void ShowScene(Scene scene);
        void ShowError(string message);
        int GetUserChoice(int maxOption);
    }

    //  для получения данных игры
    public interface IGameLoader
    {
        Game Load();
    }
}