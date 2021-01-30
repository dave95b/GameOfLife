using System.Runtime.CompilerServices;

namespace GameOfLive.Model
{
    //public readonly struct GameState
    //{
    //    public ref byte this[int x, int y] => ref State[y + 1][x + 1];

    //    public readonly int Width, Height;

    //    public readonly byte[][] State;

    //    public GameState(int width, int height) : this()
    //    {
    //        Width = width;
    //        Height = height;

    //        State = new byte[height + 2][];
    //        for (int i = 0; i < height + 2; i++)
    //            State[i] = new byte[width + 2];
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public void Activate(int x, int y) => this[x, y] = 1;
    //}

    // For compute shader
    public readonly struct GameState
    {
        public ref int this[int x, int y] => ref State[(x + 1) + (y + 1) * (Width + 2)];

        public readonly int Width, Height;

        public readonly int[] State;

        public GameState(int width, int height) : this()
        {
            Width = width;
            Height = height;

            State = new int[(height + 2) * (width + 2)];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Activate(int x, int y) => this[x, y] = 1;
    }
}