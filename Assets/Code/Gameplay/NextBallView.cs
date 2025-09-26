using UnityEngine;

namespace Code.Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class NextBallView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ShowNextBall(Color color)
        {
            _spriteRenderer.color = color;
        }
    }
}