using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMove : MonoBehaviour, ISavedProgress
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

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.worldData.positionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() == progress.worldData.positionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.worldData.positionOnLevel.Position;
                
                if (savedPosition != null) 
                    Warp(to: savedPosition);
            }
        }

        private static string CurrentLevel() => 
            SceneManager.GetActiveScene().name;
 
        private void Warp(Vector3Data to)
        {
            characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(characterController.height);
            characterController.enabled = true;
        }
    }
}