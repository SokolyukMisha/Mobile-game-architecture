using CodeBase.StaticData;

namespace CodeBase.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        MonsterStaticData ForMonster(MonsterTypeID typeId);
        void Load(); 
        LevelStaticData ForLevel(string sceneKey);
    }
}