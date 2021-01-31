using GameOfLive.Model;
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

            shader.Dispatch(0, 16, 1, 1);
            resultGsBuffer.GetData(result.State);

            return result;
        }
    }
}