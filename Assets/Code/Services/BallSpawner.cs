using System.Collections.Generic;
using Code.Gameplay;
using Code.Services.Pool;
using UnityEngine;

namespace Code.Services
{
    public class BallSpawner : IBallSpawner
    {
        private Dictionary<BallTypes, Color> _ballConfigs;
        private List<BallTypes> _ballTypes;

        private Transform _ballSpawnPoint;
        private IInputService _inputService;
        private IBallFactory _ballFactory;
        private IRandomService _random;
        private NextBallView _nextBallView;
        private Color _nextBallColor;
        private BallTypes _nextBallType;

        public BallSpawner(
            Transform ballSpawnPoint,
            IInputService inputService,
            IRandomService random,
            IBallFactory ballFactory,
            NextBallView nextBallView)
        {
            _ballSpawnPoint = ballSpawnPoint;
            _inputService = inputService;
            _random = random;
            _ballFactory = ballFactory;
            _nextBallView = nextBallView;

            _inputService.OnClick += SpawnBall;
            _ballTypes = Resources.Load<BallConfigs>("Configs/BallConfigs").GetBallTypes();
            _ballConfigs = Resources.Load<BallConfigs>("Configs/BallConfigs").GetData();

            NextBallType();
            NextBallColor(_nextBallType);
        }

        public void Disable()
        {
            _inputService.OnClick -= SpawnBall;
        }

        public void SpawnBall()
        {
            Ball ball = _ballFactory.CreateBall(_nextBallColor, _nextBallType);

            ball.transform.position = _ballSpawnPoint.position;
            ball.transform.rotation = _ballSpawnPoint.rotation;
            
            NextBallType();
            NextBallColor(_nextBallType);
        }

        private void NextBallColor(BallTypes ballType)
        {
            _nextBallColor = _ballConfigs[ballType];
            _nextBallView.ShowNextBall(_nextBallColor);
        }

        private void NextBallType()
        {
            _nextBallType = _ballTypes[_random.Range(0, _ballTypes.Count)];
        }
    }
}