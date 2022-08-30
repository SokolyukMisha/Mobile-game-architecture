using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        private HeroState _heroState;
        public HeroAnimator animator;
        public event Action HealthChanged;

        public float Current
        {
            get => _heroState.currentHp;
            set
            {
                if (_heroState.currentHp != value)
                {
                    _heroState.currentHp = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _heroState.maxHp;
            set => _heroState.maxHp = value;
        }


        public void LoadProgress(PlayerProgress progress)
        {
            _heroState = progress.heroState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.heroState.currentHp = Current;
            progress.heroState.maxHp = Max;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;
            animator.PlayHit();
        }
    }
}