using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour, InputActions.IMovementMapActions
    {
        [SerializeField] private bool IsGravityOn = true;
        [SerializeField] private float GravityAcceleration = -9.8f;
        [SerializeField] private float GravityMultiplyer = 0.0001f;
        [SerializeField] private float MovementSpeed = 1;
        
        private CharacterController _controller;
        private InputActions _input;

        private Vector3 _moveDirection;
        private float _velocity;
        private bool _canMove;

        void Start()
        {
            _canMove = true;
            
            if (_input == null)
            {
                _input = new InputActions();
            }
            
            _input.MovementMap.AddCallbacks(this);
            _input.Enable();

            if (!_controller)
            {
                _controller = GetComponent<CharacterController>();
            }

            _velocity = 0;

            _controller.enabled = true;
            _moveDirection = Vector3.zero;
        }

        private void FixedUpdate()
        {
            if (IsGravityOn)
            {
                ApplyGravity();
            }

            MoveController();
        }

        private void MoveController()
        {
            _controller.Move(_moveDirection);
        }

        private void ApplyGravity()
        {
            if (!_controller.isGrounded)
            {
                _velocity += GravityAcceleration * GravityMultiplyer;
                _moveDirection += new Vector3(0, _velocity, 0);
            }
            else _velocity = 0;
        }

        public void SetAbilityToMove(bool canIt)
        {
            _canMove = canIt;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 inputValue = context.ReadValue<Vector2>();
            _moveDirection = new Vector3(inputValue.x, 0, inputValue.y) * MovementSpeed;
        }
    }
}