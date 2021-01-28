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

            ShapeCreator.Glider(gameState,  5, 1);

            int x = 5, y = 6;
            ShapeCreator.Diehard(gameState, x, y);
            ShapeCreator.Diehard(gameState, x, y + 4);
            ShapeCreator.Diehard(gameState, x, y + 8);
            ShapeCreator.Diehard(gameState, x, y + 11);
            ShapeCreator.Diehard(gameState, x + 10, y);
            ShapeCreator.Diehard(gameState, x + 10, y + 4);
            ShapeCreator.Diehard(gameState, x + 10, y + 8);
            ShapeCreator.Diehard(gameState, x + 10, y + 11);
            ShapeCreator.Diehard(gameState, x + 20, y);
            ShapeCreator.Diehard(gameState, x + 20, y + 4);
            ShapeCreator.Diehard(gameState, x + 20, y + 8);
            ShapeCreator.Diehard(gameState, x + 20, y + 11);
            ShapeCreator.Diehard(gameState, x + 30, y);
            ShapeCreator.Diehard(gameState, x + 30, y + 4);
            ShapeCreator.Diehard(gameState, x + 30, y + 8);
            ShapeCreator.Diehard(gameState, x + 30, y + 11);

            x = 5;
            y = 22;
            ShapeCreator.Diehard(gameState, x, y);
            ShapeCreator.Diehard(gameState, x, y + 4);
            ShapeCreator.Diehard(gameState, x, y + 8);
            ShapeCreator.Diehard(gameState, x, y + 11);
            ShapeCreator.Diehard(gameState, x + 10, y);
            ShapeCreator.Diehard(gameState, x + 10, y + 4);
            ShapeCreator.Diehard(gameState, x + 10, y + 8);
            ShapeCreator.Diehard(gameState, x + 10, y + 11);
            ShapeCreator.Diehard(gameState, x + 20, y);
            ShapeCreator.Diehard(gameState, x + 20, y + 4);
            ShapeCreator.Diehard(gameState, x + 20, y + 8);
            ShapeCreator.Diehard(gameState, x + 20, y + 11);
            ShapeCreator.Diehard(gameState, x + 30, y);
            ShapeCreator.Diehard(gameState, x + 30, y + 4);
            ShapeCreator.Diehard(gameState, x + 30, y + 8);
            ShapeCreator.Diehard(gameState, x + 30, y + 11);
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