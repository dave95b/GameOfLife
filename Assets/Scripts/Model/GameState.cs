namespace GameOfLive.Model
{
    public readonly struct GameState
    {
        public ref int this[int x, int y] => ref state[y + 1][x + 1];

        public readonly int Width, Height;

        private readonly int[][] state;

        public GameState(int width, int height) : this()
        {
            Width = width;
            Height = height;

            state = new int[height + 2][];
            for (int i = 0; i < height + 2; i++)
                state[i] = new int[width + 2];
        }
    }
}