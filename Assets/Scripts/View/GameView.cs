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
            for (int i = 0; i < images.Length; i++)
            {
                var image = images[i];
                bool state = gameState[i / gameState.Height, i % gameState.Width] == 1;
                image.enabled = state;
            }
        }

        [ContextMenu("Find images")]
        private void FindImages()
        {
            images = GetComponentsInChildren<RawImage>();
        }
    }
}