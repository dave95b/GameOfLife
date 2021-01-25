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
        private GameViewModel viewModel;

        private void Awake()
        {
            viewModel.OnGameUpdated += UpdateView;
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

        [ContextMenu("Find images")]
        private void FindImages()
        {
            images = GetComponentsInChildren<RawImage>();
        }
    }
}