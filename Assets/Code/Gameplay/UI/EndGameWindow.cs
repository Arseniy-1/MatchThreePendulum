using System;
using Code.Services.SceneLoader;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.UI
{
    public class EndGameWindow : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        
        [SerializeField] private TextMeshProUGUI _score;
        
        private ISceneLoader _sceneLoader;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(RestartLevel);
            _exitButton.onClick.AddListener(Exit);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(RestartLevel);
            _exitButton.onClick.RemoveListener(Exit);
        }

        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void ShowResult(int score)
        {
            gameObject.SetActive(true);
            _score.text = $"Score: {score}";           
        }

        private void RestartLevel()
        {
            _sceneLoader.Load(SceneManager.GetActiveScene().name);
        }

        private void Exit()
        {
            _sceneLoader.Load("MainMenu");
        }
    }
}