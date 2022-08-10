using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PositionOnLevel
    {
        public string level;
        public Vector3Data Position;

        public PositionOnLevel(string level, Vector3Data position)
        {
            this.level = level;
            Position = position;
        }
    }
}