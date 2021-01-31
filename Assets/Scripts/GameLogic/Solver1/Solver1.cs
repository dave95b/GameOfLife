using GameOfLive.Model;

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

            int max = (current.Width + 2) * (current.Height + 1) - 2;
            for (int i = current.Width + 3; i < max; i++)
            {
                int count = GetNeighbourCount(current, i);
                result[i] = (byte)((count == 3) || (count == 2 && current[i] == 1) ? 1 : 0);
            }

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