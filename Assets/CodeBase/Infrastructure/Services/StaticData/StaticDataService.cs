using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string MonstersDataPath = "StaticData/Monsters";
        
        private Dictionary<MonsterTypeID, MonsterStaticData> _monsters;

        public MonsterStaticData ForMonster(MonsterTypeID typeId) =>
            _monsters.TryGetValue(typeId, out MonsterStaticData staticData)
                ? staticData
                : null;

        public void Load()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>(MonstersDataPath)
                .ToDictionary(x => x.monsterTypeID, x => x);
        }
    }
}