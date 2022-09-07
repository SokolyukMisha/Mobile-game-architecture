using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        MonsterStaticData ForMonster(MonsterTypeID typeId);
        void Load(); 
        LevelStaticData ForLevel(string sceneKey);
        WindowConfig ForWindow(WindowID shop);
    }
}