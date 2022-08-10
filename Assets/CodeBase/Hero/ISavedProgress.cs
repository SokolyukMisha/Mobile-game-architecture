using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;

namespace CodeBase.Hero
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }

    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}