using System;

namespace CodeBase.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel positionOnLevel;

        public WorldData(string initialLevel)
        {
            positionOnLevel = new PositionOnLevel(initialLevel);
        }
    }
}