using System.Collections.Generic;
using Code.Gameplay;
using UnityEngine;

namespace Code.Services
{
    public class BallFactory : IBallFactory
    {
        private Dictionary<BallTypes, Color> _ballConfigs;
        private List<BallTypes> _ballTypes;
        
        private Ball _ballPrefab;
        
        public BallFactory()
        {
            _ballPrefab = Resources.Load<Ball>("Prefabs/Ball");
        }
        
        public Ball CreateBall(Color color, BallTypes ballType)
        {
            Ball ball = Object.Instantiate(_ballPrefab);
            ball.InitializeBallView(color, ballType);
            
            return ball;
        }
    }
}