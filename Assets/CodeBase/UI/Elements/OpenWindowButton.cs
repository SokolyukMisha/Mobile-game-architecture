using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class OpenWindowButton : MonoBehaviour
    {
        public Button button;
        public WindowID windowID;
        private IWindowService _windowService;

        public void Construct(IWindowService windowService) => 
            _windowService = windowService;

        private void Awake() => 
            button.onClick.AddListener(OpenWindow);

        private void OpenWindow() => 
            _windowService.Open(windowID);
    }
}