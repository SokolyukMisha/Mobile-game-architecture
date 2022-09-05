using System;
using UnityEngine;

namespace CodeBase.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public String id;
        public MonsterTypeID monsterTypeID;
        public Vector3 position;

        public EnemySpawnerData(string id, MonsterTypeID monsterTypeID, Vector3 position)
        {
            this.id = id;
            this.monsterTypeID = monsterTypeID;
            this.position = position;
        }
    }
}