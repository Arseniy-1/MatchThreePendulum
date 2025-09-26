using Code.Services.SceneLoader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.UI
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        [SerializeField] private Button _playButton;
        
        private ISceneLoader _sceneLoader;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(HandlePlayButtonClick);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(HandlePlayButtonClick);
        }

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        private void HandlePlayButtonClick()
        {
            _loadingCurtain.Show();
            _sceneLoader.Load("Game", OnSceneLoaded);
        }

        private void OnSceneLoaded()
        {
            _loadingCurtain.Hide();
        }
    }
}