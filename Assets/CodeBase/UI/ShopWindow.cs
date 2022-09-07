using TMPro;

namespace CodeBase.UI
{
    class ShopWindow : WindowBase
    {
        public TextMeshProUGUI skullText;

        protected override void Initialize() => 
            RefreshSkullText();

        protected override void SubscribeUpdate() => 
            Progress.worldData.lootData.Changed += RefreshSkullText;

        protected override void CleanUp()
        {
            base.CleanUp();
            Progress.worldData.lootData.Changed -= RefreshSkullText;
        }
        private void RefreshSkullText() => 
            skullText.text = Progress.worldData.lootData.collected.ToString();
    }
}