using GameOfLive.Model;
using GameOfLive.ViewModel;
using UnityEngine;

namespace GameOfLive.View
{
    public abstract class BaseView : MonoBehaviour
    {
        [SerializeField]
        private GameViewModel viewModel;

        protected virtual void Awake()
        {
            viewModel.OnGameCreated += CreateView;
            viewModel.OnGameUpdated += UpdateView;
        }

        protected abstract void CreateView(GameState gameState);
        protected abstract void UpdateView(GameState gameState);
    }
}