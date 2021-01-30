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

        [SerializeField]
        private ComputeShader computeSolver;

        private ISolver solver;
        private GameState gameState;

        private float timer;

        private float totalSolveTime = 0f;
        private int generations = 0;
        private bool first = true;

        private void Awake()
        {
            //solver = new Solver();
            solver = new ComputeSolver(computeSolver);
            solver.Init(width, height);
            gameState = new GameState(width, height);

            //ShapeCreator.Pulsar(gameState, 4, 10);
            //ShapeCreator.Pulsar(gameState, 20, 10);
            //ShapeCreator.Pulsar(gameState, 4, 30);
            //ShapeCreator.Pulsar(gameState, 20, 30);
            //ShapeCreator.Pulsar(gameState, 4, 50);
            //ShapeCreator.Pulsar(gameState, 20, 50);
            //ShapeCreator.Pulsar(gameState, 4, 70);
            //ShapeCreator.Pulsar(gameState, 20, 70);
            //ShapeCreator.Pulsar(gameState, 4, 90);
            //ShapeCreator.Pulsar(gameState, 20, 90);

            ShapeCreator.Rectangle(gameState, width-2, height-2, 1, 1);

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