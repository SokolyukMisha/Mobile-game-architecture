using CodeBase.UI.Services.Factory;

namespace CodeBase.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowID windowID)
        {
            switch (windowID)
            {
                case WindowID.Unknown:
                    break;
                case WindowID.Shop:
                    _uiFactory.CreateShopWindow();
                    break;
            }
        }
    }
}