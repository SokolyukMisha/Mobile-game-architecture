using System.Collections.Generic;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string levelKey;
        public List<EnemySpawnerData> enemySpawners;
    }
}