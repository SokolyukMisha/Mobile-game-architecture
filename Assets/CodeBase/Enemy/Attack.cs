using System.Linq;
using CodeBase.Enemy.Animation;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        public EnemyAnimator animator;

        public float attackCooldown = 2f;
        public float cleavege = 0.5f;
        public float effectiveDistance = 0.5f;

        private IGameFactory _gameFactory;
        private Transform _heroTransform;
        private float _attackCooldown;
        private bool _isAttacking = false;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;
        public float damage = 10f;

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _gameFactory.HeroCreated += OnHeroCreated;
            _layerMask = 1<<LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCooldown();
            if (CanAttack())
                StartAttack();
        }

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            animator.PlayAttack();

            _isAttacking = true;
        }

        private void OnAttack()
        {
            if(Hit(out Collider hit))
            {
                hit.transform.GetComponent<IHealth>().TakeDamage(damage);
            }
        }

        private void OnAttackEnded()
        {
            _attackCooldown = attackCooldown;
            _isAttacking = false;
        }

        private bool Hit(out Collider hit)
        {
            Vector3 position = transform.position;
            int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(position), cleavege, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitCount > 0;
        }

        private Vector3 StartPoint(Vector3 position) => 
            new Vector3(position.x, position.y + 0.5f, position.z) + transform.forward*effectiveDistance;

        private void OnHeroCreated() =>
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool CooldownIsUp() =>
            _attackCooldown <= 0;

        private bool CanAttack() =>
            _attackIsActive && _isAttacking == false && CooldownIsUp();

        public void EnableAttack() => 
            _attackIsActive = true;

        public void DisableAttack() => 
            _attackIsActive = false;

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _attackCooldown -= Time.deltaTime;
        }
    }
}