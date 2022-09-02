﻿using System.Collections.Generic;
using CodeBase.Enemy.Loot;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject CreateHero(GameObject at);
        GameObject CreateHud();
        void CleanUpCode();
        void Register(ISavedProgressReader progressReader);
        GameObject InstantiateMonster(MonsterTypeID monsterTypeID, Transform parent);
        LootPiece CreateLoot();
    }
}