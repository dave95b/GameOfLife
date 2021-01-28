using GameOfLive.Model;
using UnityEngine;

namespace GameOfLive.Logic
{
    public class Solver1 : ISolver
    {
        private GameState other;

        public void Init(int width, int height)
        {
            other = new GameState(width, height);
        }

        public GameState Solve(GameState current)
        {
            GameState result = other;

            for (int i = 0; i < current.Height; i++)
            {
                for (int j = 0; j < current.Width; j++)
                    result[j, i] = Random.value > 0.5f ? 0 : 1;
            }

            other = current;

            return result;
        }
    }

    public class Solver2 : ISolver
    {
        private GameState other;

        public void Init(int width, int height)
        {
            other = new GameState(width, height);
        }

        public GameState Solve(GameState current)
        {
            GameState result = other;

            for (int i = 0; i < current.Height; i++)
            {
                for (int j = 0; j < current.Width; j++)
                {
                    int count = GetNeighbourCount(current, j, i);

                    if (count == 3)
                        result[j, i] = 1;
                    else if (count == 2 && current[j, i] == 1)
                        result[j, i] = 1;
                    else
                        result[j, i] = 0;
                }
            }

            other = current;

            return result;
        }

        private int GetNeighbourCount(in GameState state, int x, int y)
        {
            int count = 0;

            count += state[x - 1, y - 1];
            count += state[x, y - 1];
            count += state[x + 1, y - 1];

            count += state[x - 1, y];
            count += state[x + 1, y];

            count += state[x - 1, y + 1];
            count += state[x, y + 1];
            count += state[x + 1, y + 1];

            return count;
        }
    }
}