using Assets.Scripts.Utils;
using GameOfLive.Model;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Profiling;

namespace GameOfLive.View
{
    public class RenderTextureView : BaseView
    {
        [SerializeField]
        private int size = 8, spacing = 2;

        [SerializeField]
        private ComputeShader renderShader;

        [SerializeField]
        private RenderTexture renderTexture;

        [SerializeField]
        private OnRenderImageDispatcher dispatcher;

        private ComputeBuffer currentGsBuffer;

        protected override void CreateView(GameState gameState)
        {
            int width = gameState.Width * size + (gameState.Width - 1) * spacing;
            int height = gameState.Height * size + (gameState.Height - 1) * spacing;

            renderTexture = new RenderTexture(width, height, 1);
            renderTexture.enableRandomWrite = true;
            renderTexture.Create();

            renderShader.SetTexture(0, "Result", renderTexture);

            currentGsBuffer = new ComputeBuffer((width + 2) * (height + 2), sizeof(int));
            currentGsBuffer.SetData(gameState.State);
            renderShader.SetBuffer(0, "CurrentGS", currentGsBuffer);

            renderShader.SetInt("GsWidth", gameState.Width + 2);
            renderShader.SetInt("Size", size);
            renderShader.SetInt("Spacing", spacing);
            renderShader.SetFloat("PixelWidth", width);
            renderShader.SetFloat("PixelHeight", height);

            dispatcher.OnImageRendered += OnImageRendered;

            UpdateView(gameState);
        }

        protected override void UpdateView(GameState gameState)
        {
            currentGsBuffer.SetData(gameState.State);
            renderShader.Dispatch(0, gameState.Height / 8, gameState.Width / 8, 1);
        }

        public void OnImageRendered(RenderTexture dest)
        {
            Graphics.Blit(renderTexture, dest);
        }

        private void OnDestroy()
        {
            renderTexture.Release();
        }
    }
}