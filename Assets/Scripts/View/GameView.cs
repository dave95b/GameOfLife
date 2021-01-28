using GameOfLive.Model;
using GameOfLive.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace GameOfLive.View
{
    public class GameView : MonoBehaviour
    {
        [SerializeField]
        private RawImage[] images;

        [SerializeField]
        private RawImage prefab;

        [SerializeField]
        private GameViewModel viewModel;

        [SerializeField]
        GridLayoutGroup grid;

        private void Awake()
        {
            viewModel.OnGameCreated += CreateView;
            viewModel.OnGameUpdated += UpdateView;
        }

        private void CreateView(GameState gameState)
        {
            UpdateGridProperties(gameState);
            CreateCells(gameState);

            UpdateView(gameState);
        }

        private void UpdateGridProperties(GameState gameState)
        {
            float gridSize = CalculateGridSize(gameState);

            grid.cellSize = new Vector2(gridSize, gridSize);
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = gameState.Width;
        }

        private void CreateCells(GameState gameState)
        {
            Transform parent = grid.transform;
            int count = gameState.Width * gameState.Height;
            images = new RawImage[count];

            for (int i = 0; i < count; i++)
                images[i] = Instantiate(prefab, parent);
        }

        private void UpdateView(GameState gameState)
        {
            for (int i = 0; i < gameState.Height; i++)
            {
                for (int j = 0; j < gameState.Width; j++)
                {
                    int imageIndex = i * gameState.Width + j;
                    var image = images[imageIndex];
                    bool state = gameState[j, i] == 1;
                    image.enabled = state;
                }
            }
        }

        private static float CalculateGridSize(GameState gameState)
        {
            var camera = Camera.main;
            float camHeight = camera.orthographicSize * 2;
            float camWidth = camera.aspect * camHeight;

            const int sizeFactor = 128;
            float maxHeight = sizeFactor * camHeight;
            float maxWidth = sizeFactor * camWidth;
            float gridSize = Mathf.Min(maxHeight, maxWidth) / gameState.Height;
            return gridSize;
        }

        [ContextMenu("Find images")]
        private void FindImages()
        {
            images = GetComponentsInChildren<RawImage>();
        }
    }
}