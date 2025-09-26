using Code.Infrastructure;
using UnityEngine;

namespace Code.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private BallConfigs _ballConfigs;

        public StaticDataService()
        {
            _ballConfigs = Resources.Load<BallConfigs>("Configs/BallConfigs");
        }

        public BallConfigs GetBallConfigs() => 
            _ballConfigs;
    }
}