using GameOfLive.Model;

namespace GameOfLive.Logic
{
    public interface ISolver
    {
        void Init(int width, int height);
        GameState Solve(GameState current);
    }
}