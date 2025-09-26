using System;

namespace Code.Services.SceneLoader
{
    public interface ISceneLoader
    {
        void Load(string name, Action OnLoaded = null);
    }
}