using System;
using Code.Services.Pool;
using UnityEngine;

namespace Code.Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour, IDestoyable<Ball>
    {
        private SpriteRenderer _spriteRenderer;

        public event Action<Ball> Destroyed;
        
        public Rigidbody2D Rigidbody { get; private set; }
        public BallTypes BallType { get; private set; }
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        public void InitializeBallView(Color color, BallTypes ballType)
        {
            _spriteRenderer.color = color;
            BallType = ballType;
        }

        public void Destroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}