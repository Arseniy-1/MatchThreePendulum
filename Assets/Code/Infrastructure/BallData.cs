using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Infrastructure
{
    [Serializable]
    public struct BallData
    {
        [field: SerializeField]
        [field: HideLabel]
        [field: HorizontalGroup] public BallTypes Type { get; private set; }

        [field: SerializeField]
        [field: HideLabel] 
        [field: HorizontalGroup] public Color Color { get; private set; }
        
        [field: SerializeField]
        [field: HideLabel] 
        [field: HorizontalGroup] public int Price { get; private set; }
    }
}