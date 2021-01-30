﻿using GameOfLive.Model;
using System.Diagnostics;
using UnityEngine;

namespace GameOfLive.Logic
{
    public class ComputeSolver : ISolver
    {
        private readonly ComputeShader shader;

        private GameState resultGS;
        private ComputeBuffer currentGsBuffer, resultGsBuffer;

        public ComputeSolver(ComputeShader shader) => this.shader = shader;

        public void Init(in GameState current)
        {
            int width = current.Width;
            int height = current.Height;

            resultGS = new GameState(width, height);
            currentGsBuffer = new ComputeBuffer((width + 2) * (height + 2), sizeof(int));
            resultGsBuffer = new ComputeBuffer((width + 2) * (height + 2), sizeof(int));

            shader.SetInt("Width", width + 2);
            shader.SetInt("Height", height + 2);

            currentGsBuffer.SetData(current.State);
            shader.SetBuffer(0, "CurrentGS", currentGsBuffer);

            resultGsBuffer.SetData(resultGS.State);
            shader.SetBuffer(0, "ResultGS", resultGsBuffer);
        }

        public GameState Solve(in GameState current)
        {
            currentGsBuffer.SetData(current.State);

            GameState result = resultGS;
            resultGS = current;

            //var s = Stopwatch.StartNew();
            shader.Dispatch(0, 8, 8, 1);
            //s.Stop();
            //PrintStopwatch(s, "Dispatch");

            //s.Restart();

            resultGsBuffer.GetData(result.State);
            //s.Stop();
            //PrintStopwatch(s, "GetData");

            return result;
        }

        private void PrintStopwatch(Stopwatch stopwatch, string label)
        {
            UnityEngine.Debug.LogError($"{label} time: {stopwatch.Elapsed.TotalMilliseconds} ms");
        }
    }
}