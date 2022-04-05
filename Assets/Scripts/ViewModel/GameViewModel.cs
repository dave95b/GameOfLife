using GameOfLive.Logic;
using GameOfLive.Model;
using System;
using UnityEngine;

namespace GameOfLive.ViewModel
{
    public class GameViewModel : MonoBehaviour
    {
        public event Action<GameState> OnGameCreated;
        public event Action<GameState> OnGameUpdated;

        [SerializeField]
        private int width = 20, height = 20;

        [SerializeField]
        private bool interval = true;

        [SerializeField]
        private float intervalTime = 1f;

        [SerializeField]
        private ComputeShader computeSolver;

        [SerializeField]
        private bool useShaderSolver;

        private ISolver solver;
        private GameState gameState;

        private float timer;

        private void Awake()
        {
            if (useShaderSolver)
                solver = new ComputeSolver(computeSolver);
            else
                solver = new Solver();

            gameState = new GameState(width, height);

            ShapeCreator.Randomize(gameState);
            solver.Init(gameState);

            timer = intervalTime;
        }

        private void Start()
        {
            OnGameCreated?.Invoke(gameState);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Solve();

            if (interval)
                UpdateTimer();
        }

        private void Solve()
        {
            gameState = solver.Solve(gameState);
            OnGameUpdated?.Invoke(gameState);
        }

        private void UpdateTimer()
        {
            timer -= Time.deltaTime;

            if (timer > 0.0f)
                return;

            timer = intervalTime;
            Solve();
        }
    }
}