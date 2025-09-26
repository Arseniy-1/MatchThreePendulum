using System.Collections.Generic;
using Code.Gameplay;
using Code.Infrastructure;
using Code.Services.Pool;
using Code.Services.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Services
{
    public class BallSpawner : Spawner<Ball>, IBallSpawner
    {
        private Dictionary<BallTypes, Color> _ballConfigs;
        private List<BallTypes> _ballTypes;

        private Transform _ballSpawnPoint;
        private IInputService _inputService;
        private IRandomService _random;
        private NextBallView _nextBallView;
        private Color _nextBallColor;
        private BallTypes _nextBallType;

        public BallSpawner(
            Transform parent,
            Transform ballSpawnPoint,
            IInputService inputService,
            IRandomService random,
            IBallFactory ballFactory,
            NextBallView nextBallView,
            IStaticDataService staticDataService)
            : base(ballFactory, parent)
        {
            _ballSpawnPoint = ballSpawnPoint;
            _nextBallView = nextBallView;

            _ballTypes = staticDataService.GetBallConfigs().GetBallTypes();
            _ballConfigs = staticDataService.GetBallConfigs().GetColorsData();

            _inputService = inputService;
            _random = random;
            
            _inputService.OnClick += SpawnBall;
            
            NextBallType();
            NextBallColor(_nextBallType);
        }

        public void Disable()
        {
            _inputService.OnClick -= SpawnBall;
        }

        public void SpawnBall()
        {
            Ball ball = Spawn();
            ball.InitializeBallView(_nextBallColor, _nextBallType);

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