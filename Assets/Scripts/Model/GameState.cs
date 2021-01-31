using System.Runtime.CompilerServices;

namespace GameOfLive.Model
{
    public readonly struct GameState
    {
        public ref int this[int x, int y] => ref State[(x + 1) + (y + 1) * (Width + 2)];
        public ref int this[int i] => ref State[i];

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