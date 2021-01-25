namespace GameOfLive.Model
{
    public readonly struct GameState
    {
        public ref int this[int x, int y] => ref state[x][y];

        public readonly int Width, Height;

        private readonly int[][] state;

        public GameState(int width, int height) : this()
        {
            Width = width;
            Height = height;

            state = new int[height][];
            for (int i = 0; i < width; i++)
                state[i] = new int[width];
        }
    }
}