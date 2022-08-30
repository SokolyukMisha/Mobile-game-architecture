using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeID monsterTypeID;
        private string _id;

        public bool slain;

        private void Awake()
        {
            _id = GetComponent<UniqueID>().id;
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