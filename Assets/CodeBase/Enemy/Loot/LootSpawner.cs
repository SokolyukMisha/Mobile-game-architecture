using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace CodeBase.Enemy.Loot
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath enemyDeath;
        private IGameFactory _factory;
        private int _lootMin;
        private int _lootMax;

        public void Construct(IGameFactory factory)
        {
            _factory = factory;
        }
        private void Start() => 
            enemyDeath.Happened += SpawnLoot;

        private void SpawnLoot()
        {
            LootPiece loot = _factory.CreateLoot();
            loot.transform.position = transform.position;

            var lootItem = GenerateLoot();
            loot.Initialize(lootItem);
        }

        private Data.Loot GenerateLoot()
        {
            return new Data.Loot()
            {
                value = Random.Range(_lootMin, _lootMax)
            };
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}