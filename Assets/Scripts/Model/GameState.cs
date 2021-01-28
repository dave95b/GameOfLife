namespace GameOfLive.Model
{
    public readonly struct GameState
    {
        public ref char this[int x, int y] => ref state[y + 1][x + 1];

        public readonly int Width, Height;

        private readonly char[][] state;

        public GameState(int width, int height) : this()
        {
            Width = width;
            Height = height;

            state = new char[height + 2][];
            for (int i = 0; i < height + 2; i++)
                state[i] = new char[width + 2];
        }
    }
    public readonly struct GameState2
    {
        public ref int this[int x, int y] => ref state[y + 1][x + 1];

        public readonly int Width, Height;

        private readonly int[][] state;

        public GameState2(int width, int height) : this()
        {
            Width = width;
            Height = height;

            state = new int[height + 2][];
            for (int i = 0; i < height + 2; i++)
                state[i] = new int[width + 2];
        }
    }
}