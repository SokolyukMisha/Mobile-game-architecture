using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _allServices;
        private const string Initial = "Initial";
        private const string Main = "Main";

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices allServices)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _allServices = allServices;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
            
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>(Main);

        private void RegisterServices()
        {
            _allServices.RegisterSingle(SetupInputService());
            _allServices.RegisterSingle<IAssets>(new AssetProvider());
            _allServices.RegisterSingle<IGameFactory>(new GameFactory(_allServices.Single<IAssets>()));

        }

        private static IInputService SetupInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}