using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "Static Data/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeID monsterTypeID;
        
        [Range(1, 10)]
        public float attackSpeed;
        
        [Range(1, 100)]
        public int maxLoot;
        
        [Range(1, 100)]
        public int minLoot;
        
        [Range(1, 100)]
        public int hp = 20;
        
        [Range(1, 30)]
        public int damage = 2;
        
        [Range(0.5f, 1)]
        public float effectiveDistance = 0.6f;
        
        [Range(0.5f, 1)]
        public float cleavage = 0.6f;
        
        [Range(1, 20)]
        public float moveSpeed;
        
        public GameObject prefab;
    }
}