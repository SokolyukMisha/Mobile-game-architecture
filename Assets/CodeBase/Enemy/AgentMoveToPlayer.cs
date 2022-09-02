using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        public NavMeshAgent agent;
        private Transform _heroTransform;
        public void Construct(Transform player)
        {
            _heroTransform = player;
        }

        private void Update() => 
            SetDestinationForAgent();

        private void SetDestinationForAgent()
        {
            if (_heroTransform)
            {
                agent.destination = _heroTransform.position;
            }
        }
    }
}