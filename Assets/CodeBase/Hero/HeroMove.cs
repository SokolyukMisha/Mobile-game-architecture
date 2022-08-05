using CodeBase.CameraLogic;
using CodeBase.Infrastructure;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroMove : MonoBehaviour
    {
        private readonly int _moveMesh = Animator.StringToHash("Walking");
        public CharacterController CharacterController;
        public float movementSpeed;
        private IInputService _inputService;
        private Camera _camera;
        public Animator animator;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start()
        {
            _camera = Camera.main;

            CameraFollow();
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;
            
            if(_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize(); // Normalize the movement vector to avoid diagonal movement.
                
                transform.forward = movementVector; // Set the forward direction of the hero.
            }

            movementVector += Physics.gravity; // Add gravity to the movement vector.
            
            CharacterController.Move(movementSpeed * movementVector * Time.deltaTime);
            animator.SetFloat(_moveMesh, CharacterController.velocity.magnitude, 0.1f, Time.deltaTime);
        }

        private void CameraFollow() =>
            _camera.GetComponent<CameraFollow>().Follow(gameObject);
    }
}