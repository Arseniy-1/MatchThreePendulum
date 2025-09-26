using TMPro;
using UnityEngine;

namespace Code.Infrastructure
{
    public class ScoreHolder : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreView;
        
        private BallMatchHandler _ballMatchHandler;
        
        public int CurrentScore { get; private set; }

        public void Initialize(BallMatchHandler ballMatchHandler)
        {
            _ballMatchHandler = ballMatchHandler;

            _ballMatchHandler.BallMatched += OnBallMatched;
        }

        public void Disable()
        {
            _ballMatchHandler.BallMatched -= OnBallMatched;
        }

        private void OnBallMatched(int scoreAmount)
        {
            if(scoreAmount <= 0)
                return;
            
            CurrentScore += scoreAmount;
            _scoreView.text = CurrentScore.ToString(); 
        }
    }
}