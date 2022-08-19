using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        public  event Action<Collider> TriggerEnter;
        public  event Action<Collider> TriggerExit;
        private void OnTriggerEnter(Collider obj) => 
            TriggerEnter?.Invoke(obj);

        private void OnTriggerExit(Collider obj) =>
            TriggerExit?.Invoke(obj);   
    }
}
