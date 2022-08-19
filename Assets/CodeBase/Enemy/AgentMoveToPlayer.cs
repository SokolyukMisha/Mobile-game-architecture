using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private const float MinimalDistance = 1;
        
        public NavMeshAgent agent;
        private Transform _hero;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            
            if(_gameFactory.HeroGameObject != null)
                InitializeHeroTransform();
            else
            {
                _gameFactory.HeroCreated += InitializeHeroTransform;
            }
        }

        private void Update()
        {
            if(HeroInitialized() && HeroNotReached())
                agent.SetDestination(_hero.position);
        }

        private bool HeroInitialized()
        {
            return _hero.transform != null;
        }

        private void InitializeHeroTransform()
        {
            _hero = _gameFactory.HeroGameObject.transform;
        }

        private bool HeroNotReached() => 
            Vector3.Distance(agent.transform.position, _hero.position) >= MinimalDistance;
    }
}