using Code.Services;
using Code.Services.SceneLoader;
using Code.Services.StaticData;
using Zenject;

namespace Code.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            BindServices();
            BindFactories();
            DontDestroyOnLoad(this);
        }

        private void BindServices()
        {
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle().NonLazy();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();
        }
        
        private void BindFactories()
        {
            Container.Bind<IBallFactory>().To<BallFactory>().AsSingle();
        }
    }
}