using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code
{
    [CreateAssetMenu(menuName = "Configs/BallConfigs", fileName = "BallConfigs", order = 51)]
    public class BallConfigs : ScriptableObject
    {
        [SerializeField] private BallData[] _ballsData;
        
        public Dictionary<BallTypes, Color> GetData()
        {
            return _ballsData.ToDictionary(x => x.Type, x => x.Color);
        }
        
        public List<BallTypes> GetBallTypes()
        {
            return _ballsData.Select(x => x.Type).ToList();
        }
    }
}