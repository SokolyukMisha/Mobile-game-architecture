using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class LootCounter : MonoBehaviour
    {
        public TextMeshProUGUI counter;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.lootData.Changed += UpdateCounter;
        }

        private void Start()
        {
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            counter.text = $"{_worldData.lootData.collected}";
        }
    }
}