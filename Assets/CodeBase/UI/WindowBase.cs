using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public abstract class WindowBase : MonoBehaviour
    {
        public Button closeButton;
        protected IPersistentProgressService ProgressService;
        protected PlayerProgress Progress => ProgressService.Progress;
        

        public void Construct(IPersistentProgressService progressService) => 
            ProgressService = progressService;

        private void Awake() =>
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdate();
        }

        private void OnDestroy() =>
            CleanUp();

        protected virtual void OnAwake() =>
            closeButton.onClick.AddListener(() => Destroy(gameObject));

        protected virtual void Initialize()
        {
        }

        protected virtual void SubscribeUpdate()
        {
        }

        protected virtual void CleanUp()
        {
        }
    }
}