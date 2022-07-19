using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    { 
        [SerializeField] private Transform _following;
        
        public float rotationAngleX;
        public int distance;
        public float offsetY;

        private void Update()
        {
            if (_following == null) 
                return;

            Quaternion rotation = Quaternion.Euler(rotationAngleX, 0, 0);
            
            Vector3 position = rotation * new Vector3(0, 0, -distance) + FollowingPointPosition();

            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject following) => 
            _following = following.transform;

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += offsetY;
            return followingPosition;
        }
    }
}