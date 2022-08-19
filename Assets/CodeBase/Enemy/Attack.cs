using System;
using CodeBase.Enemy.Animation;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        public EnemyAnimator animator;
        
        private IGameFactory _gameFactory;
        private Transform _heroTransform;

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _gameFactory.HeroCreated += OnHeroCreated;
        }

        private void OnHeroCreated()
        {
            _heroTransform = _gameFactory.HeroGameObject.transform;
        }
    }
}