using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Enemy.Loot;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progress;
        private readonly IWindowService _windowService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public List<ISavedProgress> ProgressWriters { get; } = new();

        private GameObject HeroGameObject { get; set; }


        public GameFactory(IAssets assets, IStaticDataService staticData, IPersistentProgressService progress, IWindowService windowService)
        {
            _assets = assets;
            _staticData = staticData;
            _progress = progress;
            _windowService = windowService;
        }

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiateRegistered(Constants.HeroPrefabPath, at.transform.position);
            return HeroGameObject;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(Constants.HudPrefabPath);
            hud.GetComponentInChildren<LootCounter>().Construct(_progress.Progress.worldData);

            foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
            {
                openWindowButton.Construct(_windowService);
            }
            return hud;
        }

        public void CleanUpCode()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, position);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        public GameObject InstantiateMonster(MonsterTypeID monsterTypeID, Transform parent)
        {
            MonsterStaticData monsterStaticData = _staticData.ForMonster(monsterTypeID);
            GameObject monster =
                Object.Instantiate(monsterStaticData.prefab, parent.position, Quaternion.identity, parent);
            var health = monster.GetComponent<IHealth>();
            health.Current = monsterStaticData.hp;
            health.Max = monsterStaticData.hp;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterStaticData.moveSpeed;

            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.SetLoot(monsterStaticData.minLoot, monsterStaticData.maxLoot);
            lootSpawner.Construct(this);

            var attack = monster.GetComponent<Attack>();
            attack.Construct(HeroGameObject.transform);
            attack.attackCooldown = monsterStaticData.attackSpeed;
            attack.damage = monsterStaticData.damage;
            attack.cleavege = monsterStaticData.cleavage;
            attack.effectiveDistance = monsterStaticData.effectiveDistance;

            return monster;
        }

        public LootPiece CreateLoot()
        {
            var lootPiece = InstantiateRegistered(Constants.Loot).GetComponent<LootPiece>();
            lootPiece.Construct(_progress.Progress.worldData);
            return lootPiece;
        }

        public void CreateSpawner(Vector3 at, string spawnerID, MonsterTypeID spawnerMonsterTypeID)
        {
            SpawnPoint spawner = InstantiateRegistered(Constants.Spawner, at)
                .GetComponent<SpawnPoint>();
            spawner.Construct(this);
            spawner.ID = spawnerID;
            spawner.monsterTypeID = spawnerMonsterTypeID;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
    }
}