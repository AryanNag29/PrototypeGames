using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace PrototypeGames
{
    public class PlayerMovement : MonoBehaviour 
    {
        //頑張って
        #region References

        

        #endregion

        #region Variables

        [SerializeField] private float _speedMultiplier = 5.0f;
        private float _horizontalInput;
        private float _verticalInput;
        private Vector3 _direction;

        #endregion

        #region Functions

        public void OnMove(InputAction.CallbackContext context)
        {
            _direction = context.ReadValue<Vector2>();
        }

        #endregion

        #region Start/Update
        
        void Start()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
        }
        
        void Update()
        {
            _horizontalInput = Input.GetAxis("Horizontal"); 
            _verticalInput = Input.GetAxis("Vertical");
            transform.Translate(_direction*(_speedMultiplier*Time.deltaTime));
        }
        #endregion
    }
}
