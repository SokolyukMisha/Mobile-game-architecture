using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class HpBar : MonoBehaviour
    {
        public Image imageCurrent;

        public void SetValue(float current, float max) =>
            imageCurrent.fillAmount = current / max;
    }
}