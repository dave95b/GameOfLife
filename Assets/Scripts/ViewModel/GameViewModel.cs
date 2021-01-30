using GameOfLive.Logic;
using GameOfLive.Model;
using System;
using System.Diagnostics;
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

        private ISolver solver;
        private GameState gameState;

        private float timer;

        private float totalSolveTime = 0f;
        private int generations = 0;
        private bool first = true;

        private void Awake()
        {
            solver = new Solver();
            solver.Init(width, height);
            gameState = new GameState(width, height);

            ShapeCreator.Pulsar(gameState, 4, 40);
            ShapeCreator.Pulsar(gameState, 20, 40);
            ShapeCreator.Pulsar(gameState, 4, 60);
            ShapeCreator.Pulsar(gameState, 20, 60);
            ShapeCreator.Pulsar(gameState, 4, 80);
            ShapeCreator.Pulsar(gameState, 20, 80);
            ShapeCreator.Pulsar(gameState, 4, 100);
            ShapeCreator.Pulsar(gameState, 20, 100);
            ShapeCreator.Pulsar(gameState, 4, 120);
            ShapeCreator.Pulsar(gameState, 20, 120);
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
            var s = Stopwatch.StartNew();
            gameState = solver.Solve(gameState);
            s.Stop();

            if (!first)
            {
                float solveTime = (float)s.Elapsed.TotalMilliseconds;
                totalSolveTime += solveTime;
                generations++;

                UnityEngine.Debug.LogError($"Solve time: {solveTime} ms | Average: {totalSolveTime / generations} ms");
            }

            OnGameUpdated?.Invoke(gameState);
            first = false;
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