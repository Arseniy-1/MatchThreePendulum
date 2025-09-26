using System.Collections.Generic;
using Code.Gameplay;
using Code.Infrastructure;
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
        
        public Ball Create()
        {
            Ball ball = Object.Instantiate(_ballPrefab);
            
            return ball;
        }
    }
}