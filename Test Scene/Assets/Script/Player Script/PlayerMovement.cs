using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace PrototypeGames
{
    public class PlayerMovement : MonoBehaviour 
    {
        //頑張って

        #region References

        [SerializeField] protected CharacterController controls;

        #endregion

        #region Variables

        //imp variables
        private InputSystem_Actions _playerAction;

        [Header("Movement")] [SerializeField] private float _accelrationFactor = 5.0f;
        [SerializeField] private float _deaccelerationFactor = 1;
        [SerializeField] private float _playermaxSpeed = 5f;
        private float _currentSpeed;
        private float _moveInputx;
        private float _moveInputz;
        private Vector2 _input;
        private Vector3 _currentMovement;
        private bool isMovementPressed;

        //Rotation
        [Header("Rotation")] private Vector2 _inputRotation;
        private Vector3 _currentRotation;
        private bool isRotationPressed;

        #endregion

        #region Functions

        #region GatherInput

        public void OnMove(InputAction.CallbackContext context)
        {
            _input = context.ReadValue<Vector2>();
            _currentMovement.x = _input.x;
            _currentMovement.z = _input.y;
            isMovementPressed = _input.x != 0 || _input.y != 0;
        }

        protected void GatherInputOnRotation(InputAction.CallbackContext context)
        {
            _inputRotation = context.ReadValue<Vector2>();
            _currentRotation.x = _inputRotation.x;
            _currentRotation.y = _inputRotation.y;
        }

        #endregion

        #region Movement

        private void PlayerMovementUpdate()
        {
            controls.Move(_currentMovement * _currentSpeed * Time.deltaTime);
        }

        private void CalculateSpeed()
        {
            if (isMovementPressed && _currentSpeed > 0)
            {
                _currentSpeed -= _deaccelerationFactor * Time.deltaTime;
            }
            else if (isMovementPressed && _currentSpeed < _playermaxSpeed)
            {
                _currentSpeed += _accelrationFactor * Time.deltaTime;
            }

            _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _playermaxSpeed);
        }

        #endregion

        #endregion

        #region Awake

        private void Awake()
        {
            controls = GetComponent<CharacterController>();
            // InputAwake();
            _playerAction = new InputSystem_Actions();
            
            //movement
            //keyboard invoke
            _playerAction.Player.Move.started += OnMove;
            //keyboard cancel
            _playerAction.Player.Move.canceled += OnMove;
            //gamepad
            _playerAction.Player.Move.performed += OnMove;
        }

        #endregion

        #region OnEnable/OnDisable

        private void OnEnable()
        {
            _playerAction.Player.Enable();
        }
        
        private void OnDisable()
        {
            _playerAction.Player.Disable();
        }

        #endregion

        #region Start/Update

        void Start()
        {
        }

        void Update()
        {
            CalculateSpeed();
            PlayerMovementUpdate();
            Debug.Log(_currentMovement);
        }

        #endregion
    }
}