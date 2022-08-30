﻿using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData worldData;
        public HeroState heroState;
        public Stats heroStats;

        public PlayerProgress(string initialLevel)
        { 
            worldData = new WorldData(initialLevel);
            heroState = new HeroState();
            heroStats = new Stats();
        }
    }
}