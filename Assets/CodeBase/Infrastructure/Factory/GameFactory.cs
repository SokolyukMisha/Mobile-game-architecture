using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }
        public GameObject CreateHero(GameObject at) =>
            _assets.Instantiate(Constants.HeroPrefabPath, at.transform.position);

        public void CreateHud() =>
            _assets.Instantiate(Constants.HudPrefabPath);
    }
}