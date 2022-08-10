using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroMove : MonoBehaviour
    {
        private readonly int _moveMesh = Animator.StringToHash("Walking");
        public CharacterController characterController;
        public float movementSpeed;
        private IInputService _inputService;
        private Camera _camera;
        public Animator animator;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Start()
        {
            _camera = Camera.main;
            
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
            
            characterController.Move(movementSpeed * movementVector * Time.deltaTime);
            animator.SetFloat(_moveMesh, characterController.velocity.magnitude, 0.1f, Time.deltaTime);
        }
        
    }
}