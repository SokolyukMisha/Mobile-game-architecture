using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeID monsterTypeID;

        private IGameFactory _factory;

        public bool slain;

        private EnemyDeath _enemyDeath;

        public string ID { get; set; }

        public void Construct(IGameFactory factory) =>
            _factory = factory;

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.killData.ClearedSpawners.Contains(ID))
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

        private void Slay()
        {
            if (_enemyDeath != null) _enemyDeath.Happened -= Slay;
            slain = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (slain)
            {
                progress.killData.ClearedSpawners.Add(ID);
            }
        }
    }
}