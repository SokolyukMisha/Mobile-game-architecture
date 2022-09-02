using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeID monsterTypeID;
        private string _id;
        private IGameFactory _factory;
        public bool slain;
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            _id = GetComponent<UniqueID>().id;
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.killData.ClearedSpawners.Contains(_id))
                slain = true;
            else
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            GameObject monster = _factory.InstantiateMonster(monsterTypeID, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void OnDestroy()
        {
        }

        private void Slay()
        {
            if (_enemyDeath != null) _enemyDeath.Happened -= Slay;
            slain = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (slain)
            {
                progress.killData.ClearedSpawners.Add(_id);
            }
        }
    }
}