using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string MonstersDataPath = "StaticData/Monsters";
        private const string LevelsDataPath = "StaticData/Levels";
        private const string WindowDataPath = "StaticData/UI/WindowStaticData";

        private Dictionary<MonsterTypeID, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowID, WindowConfig> _windowConfigs;


        public void Load()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>(MonstersDataPath)
                .ToDictionary(x => x.monsterTypeID, x => x);

            _levels = Resources
                .LoadAll<LevelStaticData>(LevelsDataPath)
                .ToDictionary(x => x.levelKey, x => x);

            _windowConfigs = Resources
                .Load<WindowStaticData>(WindowDataPath)
                .configs.ToDictionary(x => x.windowID, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeID typeId) =>
            _monsters.TryGetValue(typeId, out MonsterStaticData staticData)
                ? staticData
                : null;

        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
                ? staticData
                : null;

        public WindowConfig ForWindow(WindowID windowID) =>
            _windowConfigs.TryGetValue(windowID, out WindowConfig windowConfig)
                ? windowConfig
                : null;
    }
}