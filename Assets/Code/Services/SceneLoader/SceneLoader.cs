using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action OnLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(name, OnLoaded));
        }

        private IEnumerator LoadScene(string name, Action OnLoaded = null)
        {
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);

            while (waitNextScene.isDone == false)
                yield return null;

            OnLoaded?.Invoke();
        }
    }
}