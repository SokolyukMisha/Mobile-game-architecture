using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        public HeroHealth heroHealth;
        public HeroMove heroMove;
        public HeroAttack heroAttack;
        public HeroAnimator heroAnimator;
        public GameObject deathEffect;
        private bool _isDead;

        private void Start() =>
            heroHealth.HealthChanged += OnHealthChanged;

        private void OnDestroy() =>
            heroHealth.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if(!_isDead && heroHealth.Current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;
            heroMove.enabled = false;
            heroAttack.enabled = false;
            heroAnimator.PlayDeath();
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            
        }
    }
}