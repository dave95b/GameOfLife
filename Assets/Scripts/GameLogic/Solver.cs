using GameOfLive.Model;
using System.Threading.Tasks;

namespace GameOfLive.Logic
{
    public class Solver : ISolver
    {
        private GameState other;

        public void Init(in GameState current)
        {
            int width = current.Width;
            int height = current.Height;

            other = new GameState(width, height);
        }

        public GameState Solve(in GameState current)
        {
            GameState result = other;
            GameState current2 = current;

            int max = (current.Width + 2) * (current.Height + 1) - 2;

            Parallel.For(current.Width + 3, max, i =>
            {
                int count = GetNeighbourCount(current2, i);
                result[i] = (byte)((count == 3) || (count == 2 && current2[i] == 1) ? 1 : 0);
            });

            other = current;

            return result;
        }

        private int GetNeighbourCount(in GameState state, int i)
        {
            int w = state.Width + 2;
            return state[i - 1 - w]
                + state[i - w]
                + state[i + 1 - w]
                + state[i - 1]
                + state[i + 1]
                + state[i - 1 + w]
                + state[i + w]
                + state[i + 1 + w];
        }
    }
}