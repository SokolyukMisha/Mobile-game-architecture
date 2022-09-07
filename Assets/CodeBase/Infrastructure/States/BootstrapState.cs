using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
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
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterStaticData();

            _allServices.RegisterSingle(SetupInputService());
            _allServices.RegisterSingle<IAssets>(new AssetProvider());
            _allServices.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            
            RegisterUI();
            
            _allServices.RegisterSingle<IGameFactory>(new GameFactory(_allServices.Single<IAssets>(),
                _allServices.Single<IStaticDataService>(), _allServices.Single<IPersistentProgressService>(),
                _allServices.Single<IWindowService>()));
            _allServices.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                _allServices.Single<IPersistentProgressService>(),
                _allServices.Single<IGameFactory>()));
        }

        private void RegisterUI()
        {
            _allServices.RegisterSingle<IUIFactory>(new UIFactory(_allServices.Single<IAssets>(),
                _allServices.Single<IStaticDataService>(), _allServices.Single<IPersistentProgressService>()));
            _allServices.RegisterSingle<IWindowService>(new WindowService(_allServices.Single<IUIFactory>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _allServices.RegisterSingle(staticData);
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