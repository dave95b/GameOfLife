using GameOfLive.Logic;
using GameOfLive.Model;
using System;
using System.Diagnostics;
using UnityEngine;

namespace GameOfLive.ViewModel
{
    public class GameViewModel : MonoBehaviour
    {
        public event Action<GameState> OnGameUpdated;

        [SerializeField]
        private bool interval = true;

        [SerializeField]
        private float intervalTime = 1f;

        private ISolver solver;
        private GameState gameState;

        private float timer;

        private float totalSolveTime = 0f;
        private int generations = 0;

        private void Awake()
        {
            solver = new Solver2();
            solver.Init(20, 20);
            gameState = new GameState(20, 20);

            Init();
            timer = intervalTime;
        }

        private void Start()
        {
            OnGameUpdated?.Invoke(gameState);
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
            float solveTime = (float) s.Elapsed.TotalMilliseconds;
            totalSolveTime += solveTime;
            generations++;

            UnityEngine.Debug.LogError($"Solve time: {solveTime} ms | Average: {totalSolveTime / generations} ms");

            OnGameUpdated?.Invoke(gameState);
        }

        private void Init()
        {
            gameState[7, 9] = 1;
            gameState[9, 10] = 1;

            gameState[6, 11] = 1;
            gameState[7, 11] = 1;
            gameState[10, 11] = 1;
            gameState[11, 11] = 1;
            gameState[12, 11] = 1;
        }

        private void Init3()
        {
            gameState[6, 6] = 1;
            gameState[7, 6] = 1;
            gameState[8, 6] = 1;
            gameState[6, 7] = 1;

            gameState[8, 8] = 1;
            gameState[7, 9] = 1;
            gameState[8, 9] = 1;
            gameState[6, 9] = 1;
        }

        private void Init2() // glider
        {
            gameState[2, 1] = 1;
            gameState[3, 2] = 1;
            gameState[1, 3] = 1;
            gameState[2, 3] = 1;
            gameState[3, 3] = 1;
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