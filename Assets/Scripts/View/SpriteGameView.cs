using GameOfLive.Model;
using System.Diagnostics;
using UnityEngine;

namespace GameOfLive.View
{
    public class SpriteGameView : BaseView
    {
        [SerializeField]
        private SpriteRenderer prefab;

        [SerializeField]
        private Transform parent;

        [SerializeField]
        private float size = 0.2f, spacing = 0.05f;

        [SerializeField]
        private new Camera camera;

        private SpriteRenderer[] items;

        protected override void CreateView(GameState gameState)
        {
            int count = gameState.Width * gameState.Height;
            items = new SpriteRenderer[count];

            var startPosition = GetCameraTopLeftPosition() + new Vector2(size / 2, -size / 2);
            CreateSprites(gameState, startPosition);
            UpdateView(gameState);
        }

        protected override void UpdateView(GameState gameState)
        {
            int imageIndex = 0;
            for (int i = 0; i < gameState.Height; i++)
            {
                for (int j = 0; j < gameState.Width; j++)
                {
                    var image = items[imageIndex];
                    bool state = gameState[j, i] == 1;
                    image.enabled = state;
                    imageIndex++;
                }
            }
        }

        private Vector2 GetCameraTopLeftPosition()
        {
            float y = camera.orthographicSize;
            float x = camera.aspect * y;

            return new Vector2(-x, y);
        }

        private void CreateSprites(in GameState gameState, in Vector2 startPosition)
        {
            int itemCount = 0;
            for (int y = 0; y < gameState.Height; y++)
            {
                float yOffset = y * (size + spacing);
                for (int x = 0; x < gameState.Width; x++)
                {
                    float xOffset = x * (size + spacing);
                    Vector2 position = startPosition + new Vector2(xOffset, -yOffset);
                    items[itemCount] = Create(position);
                    itemCount++;
                }
            }
        }

        private SpriteRenderer Create(in Vector2 position)
        {
            var created = Instantiate(prefab, position, Quaternion.identity, parent);
            created.transform.localScale = new Vector3(size, size, 1f);

            return created;
        }
    }
}