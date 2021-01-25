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

            for (int i = 0; i < current.Width; i++)
            {
                for (int j = 0; j < current.Height; j++)
                    result[i, j] = Random.value > 0.5f ? 0 : 1;
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

            //for (int i = 0; i < current.Width; i++)
            //{
            //    for (int j = 0; j < current.Height; j++)
            //        result[i, j] = Random.value > 0.5f;
            //}

            other = current;

            return result;
        }

        private int GetNeighbourCount(int x, int y)
        {
            return 0;
        }
    }
}