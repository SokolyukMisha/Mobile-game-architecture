using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        public HeroAnimator heroAnimator;
        public CharacterController characterController;
        private IInputService _input;
        private static int layerMask;
        private Collider[] _hits = new Collider[3];
        private Stats _stats;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
            layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }
        

        private void Update()
        {
            if (_input.isAttackButtonUp() && !heroAnimator.IsAttacking)
                heroAnimator.PlayAttack();
        }


        public void LoadProgress(PlayerProgress progress) => 
            _stats = progress.heroStats;

        public void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.damage);
            }
        }

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StarPoint() + transform.forward, _stats.radius, _hits, layerMask);

        private Vector3 StarPoint() =>
            new Vector3(transform.position.x, characterController.center.y / 2, transform.position.z);
        
    }
}