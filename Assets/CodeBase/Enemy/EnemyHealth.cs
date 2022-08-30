using System;
using CodeBase.Enemy.Animation;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public EnemyAnimator animator;

        [SerializeField] private float current;
        [SerializeField] private float max;

        public event Action HealthChanged;

        public float Current
        {
            get => current;
            set => current = value;
        }

        public float Max
        {
            get => max;
            set => max = value;
        }

        public void TakeDamage(float damage)
        {
            Current -= damage;
            animator.PlayHit();
            HealthChanged?.Invoke();
        }
    }
}