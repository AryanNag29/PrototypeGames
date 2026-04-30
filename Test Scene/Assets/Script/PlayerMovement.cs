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

        //Movement
        [SerializeField] private float _speedMultiplier = 5.0f;
        private float _horizontalInput;
        private float _verticalInput;
        private Vector2 _input;
        private Vector3 _currentMovement;
        private bool isMovemntPressed;
        
        //Rotation
        private Vector2 _inputRotation;
        private Vector3 _currentRotation;
        private bool isRotationPressed;

        #endregion

        #region Functions

        public void GatherInputOnMovement(InputAction.CallbackContext context)
        {
            _input = context.ReadValue<Vector2>();
            _currentMovement.x = _input.x;
            _currentMovement.z = _input.y;
            isMovemntPressed = _input.x != 0 || _input.y != 0;
        }

        protected void GatherInputOnRotation(InputAction.CallbackContext context)
        {
            _inputRotation = context.ReadValue<Vector2>();
            _currentRotation.x = _inputRotation.x;
            _currentRotation.y = _inputRotation.y;
        }
        
        //Awake input
        private void InputAwake()
        {
            //movement
            
            //keyboard invoke
            _playerAction.Player.Move.started += GatherInputOnMovement;
            //keyboard cancel
            _playerAction.Player.Move.canceled += GatherInputOnMovement;
            //gamepad
            _playerAction.Player.Move.performed += GatherInputOnMovement;
            
        }

        #endregion

        #region Awake

        private void Awake()
        {
            controls = GetComponent<CharacterController>();
            _playerAction = new InputSystem_Actions();
            
            
            
        }

        #endregion

        #region OnEnable/OnDisable

        

        #endregion

        #region Start/Update

        void Start()
        {
        }

        void Update()
        {
        }

        #endregion
    }
}