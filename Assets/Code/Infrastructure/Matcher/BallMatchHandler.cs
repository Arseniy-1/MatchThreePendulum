using System;
using System.Collections.Generic;
using Code.Infrastructure.MessageBrokers;
using Code.Services.StaticData;
using UniRx;

namespace Code.Infrastructure
{
    public class BallMatchHandler
    {
        private readonly CompositeDisposable _disposable = new ();
        
        private Dictionary<BallTypes, int> _priceData;
        
        public event Action<int> BallMatched; 
        
        public BallMatchHandler(IStaticDataService staticDataService)
        {
            _priceData = staticDataService.GetBallConfigs().GetPriceData();
            
            MessageBrokerHolder.Game
                .Receive<M_HasMatch>()
                .Subscribe(message => HandleMatch(message.MatchType))
                .AddTo(_disposable);
        }

        public void Disable()
        {
            _disposable?.Dispose();
        }

        private void HandleMatch(BallTypes ballType)
        {
            BallMatched?.Invoke(_priceData[ballType]);
        }
    }
}