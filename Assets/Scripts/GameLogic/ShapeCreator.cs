using GameOfLive.Model;

namespace GameOfLive.Logic
{
    public static class ShapeCreator
    {
        public static void Glider(in GameState gameState, int x, int y)
        {
            gameState[x + 1, y + 0] = 1;
            gameState[x + 2, y + 1] = 1;
            gameState[x + 0, y + 2] = 1;
            gameState[x + 1, y + 2] = 1;
            gameState[x + 2, y + 2] = 1;
        }

        public static void Diehard(in GameState gameState, int x, int y)
        {
            gameState[x + 1, y + 0] = 1;
            gameState[x + 3, y + 1] = 1;
            gameState[x + 0, y + 2] = 1;
            gameState[x + 1, y + 2] = 1;
            gameState[x + 4, y + 2] = 1;
            gameState[x + 5, y + 2] = 1;
            gameState[x + 6, y + 2] = 1;
        }
    }
}