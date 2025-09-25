using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code
{
    public class BallFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<BallTypes, Color> _ballConfigs;
        [SerializeField] private Ball _ballPrefab;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _ballPrefab = Resources.Load<Ball>("Prefabs/Ball");
            _ballConfigs = Resources.Load<BallConfigs>("Configs/BallConfigs").GetData();
        }
        
        public void CreateBall(BallTypes ballType)
        {
            Ball ball = Instantiate(_ballPrefab);
            
            ball.InitializeBallView(_ballConfigs[ballType]);
        }
    }
}