using System.Collections;
using UnityEngine;

namespace CodeBase.Logic
{
    public class LoadingCurtain : MonoBehaviour
    {
        public CanvasGroup curtain;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        public void Show()
        {
            curtain.alpha = 1;
        }
        public void Hide() => 
            StartCoroutine(FadeIn());

        private IEnumerator FadeIn()
        {
            while (curtain.alpha > 0)
            {
                curtain.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }
            gameObject.SetActive(false);
        }
    }
}
