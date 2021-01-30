using GameOfLive.Model;

namespace GameOfLive.Logic
{
    public static class ShapeCreator
    {
        public static void Glider(in GameState gameState, int x, int y)
        {
            gameState.Activate(x + 1, y + 0);
            gameState.Activate(x + 2, y + 1);
            gameState.Activate(x + 0, y + 2);
            gameState.Activate(x + 1, y + 2);
            gameState.Activate(x + 2, y + 2);
        }

        public static void Diehard(in GameState gameState, int x, int y)
        {
            gameState.Activate(x + 1, y + 0);
            gameState.Activate(x + 3, y + 1);
            gameState.Activate(x + 0, y + 2);
            gameState.Activate(x + 1, y + 2);
            gameState.Activate(x + 4, y + 2);
            gameState.Activate(x + 5, y + 2);
            gameState.Activate(x + 6, y + 2);
        }

        public static void Pulsar(in GameState gameState, int x, int y)
        {
            HorizontalLine(gameState, 3, x + 2, y);
            HorizontalLine(gameState, 3, x + 8, y);

            VerticalLine(gameState, 3, x, y + 2);
            VerticalLine(gameState, 3, x + 5, y + 2);
            VerticalLine(gameState, 3, x + 7, y + 2);
            VerticalLine(gameState, 3, x + 12, y + 2);

            HorizontalLine(gameState, 3, x + 2, y + 5);
            HorizontalLine(gameState, 3, x + 8, y + 5);
            HorizontalLine(gameState, 3, x + 2, y + 7);
            HorizontalLine(gameState, 3, x + 8, y + 7);

            VerticalLine(gameState, 3, x, y + 8);
            VerticalLine(gameState, 3, x + 5, y + 8);
            VerticalLine(gameState, 3, x + 7, y + 8);
            VerticalLine(gameState, 3, x + 12, y + 8);

            HorizontalLine(gameState, 3, x + 2, y + 12);
            HorizontalLine(gameState, 3, x + 8, y + 12);
        }

        public static void HorizontalLine(in GameState gameState, int length, int x, int y)
        {
            for (int i = 0; i < length; i++)
                gameState.Activate(x + i, y);
        }

        public static void VerticalLine(in GameState gameState, int length, int x, int y)
        {
            for (int i = 0; i < length; i++)
                gameState.Activate(x, y + i);
        }

        public static void Rectangle(in GameState gameState, int width, int height, int x, int y)
        {
            for (int i = 0; i < height; i++)
                HorizontalLine(gameState, width, x, y + i);
        }
    }
}