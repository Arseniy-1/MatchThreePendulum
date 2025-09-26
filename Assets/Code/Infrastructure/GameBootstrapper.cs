using Code.Gameplay;
using Code.Gameplay.UI;
using Code.Services;
using Code.Services.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private Transform _ballSpawnPoint;
        [SerializeField] private Transform _ballsParent;
        [SerializeField] private NextBallView _nextBallView;
        [SerializeField] private ScoreHolder _scoreHolder;
        [SerializeField] private EndGameWindow _endGameWindow;
        
        private IBallFactory _ballFactory;
        private IBallSpawner _ballSpawner;
        private IRandomService _randomService;
        private IInputService _inputService;
        private BallMatchHandler _ballMatchHandler;
        private GameStatusController _gameStatusController;

        [Inject]
        private void Construct(
            IRandomService randomService,
            IInputService inputService, 
            IBallFactory ballFactory, 
            IStaticDataService staticDataService)
        {
            _nextBallView.Initialize();

            DontDestroyOnLoad(this);
            
            _randomService = randomService;
            _inputService = inputService;
            _ballFactory = ballFactory;

            _gameStatusController = new GameStatusController(_endGameWindow, _scoreHolder);
            _ballMatchHandler = new BallMatchHandler(staticDataService);
            _ballSpawner = new BallSpawner(_ballsParent, _ballSpawnPoint, _inputService, _randomService, _ballFactory, _nextBallView, staticDataService);
            
            _scoreHolder.Initialize(_ballMatchHandler);
        }

        private void OnEnable()
        {
            _inputService.Enable();
        }

        private void OnDisable()
        {
            _ballMatchHandler.Disable();
            _gameStatusController.Disable();
            _inputService.Disable();
            _ballSpawner.Disable();
            _scoreHolder.Disable();
        }

        private void Update()
        {
            _inputService.Update();
        }
    }
}