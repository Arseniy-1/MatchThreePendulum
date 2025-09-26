using Code.Gameplay;
using Code.Services;
using UnityEngine;

namespace Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private Transform _ballSpawnPoint;
        [SerializeField] private NextBallView _nextBallView;

        private IBallFactory _ballFactory;
        private IBallSpawner _ballSpawner;
        private IRandomService _randomService;
        private IInputService _inputService;

        private void Awake()
        {
            _randomService = new UnityRandomService();
            _inputService = new InputService();
            _ballFactory = new BallFactory();
            _ballSpawner = new BallSpawner(_ballSpawnPoint, _inputService, _randomService, _ballFactory, _nextBallView);
        }

        private void OnEnable()
        {
            _inputService.Enable();
        }

        private void OnDisable()
        {
            _inputService.Disable();
            _ballSpawner.Disable();
        }

        private void Update()
        {
            _inputService.Update();
        }
    }
}