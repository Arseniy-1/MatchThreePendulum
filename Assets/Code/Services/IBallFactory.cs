using Code.Gameplay;
using UnityEngine;

namespace Code.Services
{
    public interface IBallFactory
    {
        Ball CreateBall(Color color, BallTypes ballType);
    }
}