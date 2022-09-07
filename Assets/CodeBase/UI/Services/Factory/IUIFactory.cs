using CodeBase.Infrastructure.Services;

namespace CodeBase.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateShopWindow();
        void CreateUIRoot();
    }
}