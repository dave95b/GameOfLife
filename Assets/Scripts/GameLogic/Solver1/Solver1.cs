using GameOfLive.Model;
using System.Collections;
using System.Collections.Generic;

namespace GameOfLive.Logic
{
    public class Solver : ISolver
    {
        private GameState other;

        private DirtyFields mainDirty, secDirty;

        public void Init(int width, int height)
        {
            other = new GameState(width, height);
            mainDirty = new DirtyFields(width, height);
            secDirty = new DirtyFields(width, height);

            mainDirty.Fill();
        }

        public GameState Solve(GameState current)
        {
            GameState result = other;
            secDirty.Clear();

            int width = current.Width;

            foreach (int i in mainDirty)
            {
                int x = i % width;
                int y = i / width;

                int count = GetNeighbourCount(current, x, y);
                int currentValue = current[x, y];
                int resultValue = 0;

                if (count == 3)
                    resultValue = 1;
                else if (count == 2 && currentValue == 1)
                    resultValue = 1;

                if (currentValue != resultValue)
                    secDirty.Add(x, y);

                result[x, y] = (char)resultValue;
            }

            other = current;

            DirtyFields temp = mainDirty;
            mainDirty = secDirty;
            secDirty = temp;

            return result;
        }

        public GameState Solve2(GameState current)
        {
            GameState result = other;

            for (int i = 0; i < current.Height; i++)
            {
                for (int j = 0; j < current.Width; j++)
                {
                    int count = GetNeighbourCount(current, j, i);
                    char resultValue = (char)0;

                    if (count == 3)
                        resultValue = (char)1;
                    else if (count == 2 && current[j, i] == 1)
                        resultValue = (char)1;

                    result[j, i] = resultValue;
                }
            }

            other = current;

            return result;
        }

        private int GetNeighbourCount(in GameState state, int x, int y)
        {
            return state[x - 1, y - 1]
                + state[x, y - 1]
                + state[x + 1, y - 1]
                + state[x - 1, y]
                + state[x + 1, y]
                + state[x - 1, y + 1]
                + state[x, y + 1]
                + state[x + 1, y + 1];
        }
    }

    internal class DirtyFields : IEnumerable<int>
    {
        private readonly List<int> list;
        private readonly HashSet<int> set;

        private readonly int width, height;

        public DirtyFields(int width, int height)
        {
            list = new List<int>(width * height);
            set = new HashSet<int>();
            (this.width, this.height) = (width, height);
        }

        public void Add(int x, int y)
        {
            TryAdd(x - 1, y - 1);
            TryAdd(x, y - 1);
            TryAdd(x + 1, y - 1);

            TryAdd(x - 1, y);
            TryAdd(x, y);
            TryAdd(x + 1, y);

            TryAdd(x - 1, y + 1);
            TryAdd(x, y + 1);
            TryAdd(x + 1, y + 1);
        }

        public void Clear()
        {
            list.Clear();
            set.Clear();
        }

        public void Fill()
        {
            int number = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    list.Add(number);
                    number++;
                }
            }
        }

        private void TryAdd(int x, int y)
        {
            if (x == -1 || x == width || y == -1 || y == height)
                return;

            int number = y * width + x;

            if (set.Add(number))
                list.Add(number);
        }

        public IEnumerator<int> GetEnumerator() => list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}