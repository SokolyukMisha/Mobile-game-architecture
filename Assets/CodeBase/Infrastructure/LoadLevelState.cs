using System;
using CodeBase.CameraLogic;
using CodeBase.Logic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string HeroPrefabPath = "Infrastructure/Hero";
        private const string HudPrefabPath = "Infrastructure/Hud";
        private const string Initialpoint = "InitialPoint";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            GameObject initialPoint = GameObject.FindGameObjectWithTag(Initialpoint);
            GameObject hero = Instantiate(HeroPrefabPath, initialPoint.transform.position);
            Instantiate(HudPrefabPath);
            
            CameraFollow(hero);
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private static GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return  Object.Instantiate(prefab);
        }
        private static GameObject Instantiate(string path, Vector3 at)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return  Object.Instantiate(prefab, at, Quaternion.identity);
        }

        private void CameraFollow(GameObject hero) =>
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}