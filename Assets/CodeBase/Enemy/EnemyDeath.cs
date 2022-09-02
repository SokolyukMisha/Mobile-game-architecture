using System;
using System.Collections;
using CodeBase.Enemy.Animation;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator), typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour
    {
        public EnemyAnimator animator;
        public EnemyHealth health;
        public NavMeshAgent agent;
        public AgentMoveToPlayer agentMoveToPlayer;
        public Attack enemyAttack;

        public GameObject deathEffect;
        public event Action Happened;

        private void Start() =>
            health.HealthChanged += OnHealthChanged;

        private void OnDestroy() =>
            health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (health.Current <= 0)
                Die();
        }

        private void Die()
        {
            health.HealthChanged -= OnHealthChanged;
            enemyAttack.enabled = false;
            agent.enabled = false;
            agentMoveToPlayer.enabled = false;
            animator.PlayDeath();
            SpawnDeathFX();
            StartCoroutine(DestroyTimer());
            Happened?.Invoke();
        }

        private void SpawnDeathFX() =>
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
}