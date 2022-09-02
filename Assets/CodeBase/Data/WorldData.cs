using System;

namespace CodeBase.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel positionOnLevel;
        public LootData lootData;

        public WorldData(string initialLevel)
        {
            positionOnLevel = new PositionOnLevel(initialLevel);
            lootData = new LootData();
        }
    }
}