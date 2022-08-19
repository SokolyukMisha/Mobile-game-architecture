using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Aggro : MonoBehaviour
    {
        public TriggerObserver triggerObserver;
        public AgentMoveToPlayer follow;

        public float cooldown;
        private Coroutine _aggroCoroutine;
        private bool _hasAgroTarget = false;

        private void Start()
        {
            triggerObserver.TriggerEnter += TriggerEnter;
            triggerObserver.TriggerExit += TriggerExit;

            SwitchFollow(false);
        }

        private void TriggerExit(Collider obj)
        {
            if (_hasAgroTarget)
            {
                _hasAgroTarget = false;
                _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());   
            }
        }

        private void TriggerEnter(Collider obj)
        {
            if (_hasAgroTarget == false)
            {
                _hasAgroTarget = true;
                StopAgroCoroutine();
                SwitchFollow(true); 
            }
        }

        private void SwitchFollow(bool b) => 
            follow.enabled = b;

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(cooldown);
            SwitchFollow(false);
        }

        private void StopAgroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(SwitchFollowOffAfterCooldown());
                _aggroCoroutine = null;
            }
        }
    }
}