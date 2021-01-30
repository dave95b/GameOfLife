using GameOfLive.Model;

namespace GameOfLive.Logic
{
    public interface ISolver
    {
        void Init(in GameState current);
        GameState Solve(in GameState current);
    }
}