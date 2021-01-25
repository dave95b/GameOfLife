using GameOfLive.Logic;
using GameOfLive.Model;
using System;
using UnityEngine;

namespace GameOfLive.ViewModel
{
    public class GameViewModel : MonoBehaviour
    {
        public event Action<GameState> OnGameUpdated;

        private ISolver solver;
        private GameState gameState;

        private void Awake()
        {
            solver = new Solver2();
            solver.Init(20, 20);
            gameState = new GameState(20, 20);

            Init();
        }

        private void Start()
        {
            OnGameUpdated?.Invoke(gameState);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameState = solver.Solve(gameState);
                OnGameUpdated?.Invoke(gameState);
            }
        }

        private void Init()
        {
            gameState[2, 1] = 1;
            gameState[3, 2] = 1;
            gameState[1, 3] = 1;
            gameState[2, 3] = 1;
            gameState[3, 3] = 1;
        }
    }
}