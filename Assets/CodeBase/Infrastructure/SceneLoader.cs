using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICouroutineRunner _couroutineRunner;

        public SceneLoader(ICouroutineRunner couroutineRunner) => 
            _couroutineRunner = couroutineRunner;

        public void Load(string name, Action onLoaded = null)
        {
            _couroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
        }

        private IEnumerator LoadScene(string name, Action onLoaded = null)
        {
            if(SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);

            while (!waitNextScene.isDone)
                yield return null;
        
            onLoaded?.Invoke();
        }
    }
}