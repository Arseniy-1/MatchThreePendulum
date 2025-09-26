using Code.Infrastructure;

namespace Code.Services.StaticData
{
    public interface IStaticDataService
    {
        BallConfigs GetBallConfigs();
    }
}