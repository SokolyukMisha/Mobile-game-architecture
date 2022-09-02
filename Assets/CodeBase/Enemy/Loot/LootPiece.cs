using System.Collections;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.Enemy.Loot
{
    public class LootPiece : MonoBehaviour, ISavedProgress
    {
        public GameObject skull;
        public GameObject pickupFXPrefab;
        public TextMeshPro lootText;
        public GameObject pickupPopup;

        private Data.Loot _loot;
        private bool _picked;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }

        public void Initialize(Data.Loot loot)
        {
            _loot = loot;
        }

        private void OnTriggerEnter(Collider other) =>
            Pickup();

        private void Pickup()
        {
            if (_picked)
                return;
            UpdateWorldData();
            HideSkull();
            PlayPickupFX();
            ShowText();

            StartCoroutine(StartDestroyTimer());
            _picked = true;
        }

        private void UpdateWorldData() => 
            _worldData.lootData.Collect(_loot);

        private void HideSkull() => 
            skull.SetActive(false);

        private void PlayPickupFX() =>
            Instantiate(pickupFXPrefab, transform.position, Quaternion.identity);

        private void ShowText()
        {
            lootText.text = $"{_loot.value}";
            pickupPopup.SetActive(true);
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }

        public void LoadProgress(PlayerProgress progress)
        {
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.worldData.lootData.collected = _loot.value;
        }
    }
}