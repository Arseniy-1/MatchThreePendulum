using System;
using Code.Infrastructure;
using UnityEngine;

namespace Code.Gameplay
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Cell : MonoBehaviour
    {
        private Ball _hookedBall;
        private BoxCollider2D _collider;

        public event Action<Cell, BallTypes> BallHooked;
        
        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(_hookedBall != null)
                return;
            
            if (other.TryGetComponent(out Ball ball))
                HookBall(ball);
        }

        public void ReleaseBall()
        {
            if(_hookedBall == null)
                return;
            
            _hookedBall.Rigidbody.isKinematic = false;
            
            _hookedBall.Destroy();
            _hookedBall = null;
        }

        public void HookBall(Ball ball)
        {
            _hookedBall = ball;
         
            if (_hookedBall.Rigidbody != null)
            {
                _hookedBall.Rigidbody.velocity = Vector2.zero;
                _hookedBall.Rigidbody.angularVelocity = 0f;
                _hookedBall.Rigidbody.isKinematic = true; 
            }
            
            _hookedBall.transform.position = _collider.transform.position;
            _hookedBall.transform.rotation = _collider.transform.rotation;
            
            BallHooked?.Invoke(this, _hookedBall.BallType);
        }

        public Ball GetBall() => 
            _hookedBall;
    }
}