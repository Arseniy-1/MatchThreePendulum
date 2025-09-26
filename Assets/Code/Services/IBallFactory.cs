using Code.Gameplay;
using UnityEngine;

namespace Code
{
    public interface IBallFactory
    {
        Ball CreateBall(Color color, BallTypes ballType);
    }
}