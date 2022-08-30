﻿using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine,
            IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, string>(_persistentProgressService.Progress.worldData
                .positionOnLevel.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            Debug.Log("Loading progress...");
            Debug.Log(_saveLoadService.LoadProgress() == null);

            _persistentProgressService.Progress =
                _saveLoadService.LoadProgress() != null ? _saveLoadService.LoadProgress() : NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            PlayerProgress progress = new PlayerProgress(initialLevel: "Main");

            progress.heroState.maxHp = 50;
            progress.heroStats.damage = 2f;
            progress.heroStats.radius = 0.5f;
            progress.heroState.ResetHp();   

            return progress;
        }
    }
}