using UnityEditor.Searcher;
using UnityEngine;

namespace PrototypeGames
{
    public class ManualPlayerInput : MonoBehaviour
    {
        #region Variables

        [Header("Movement")]
        [SerializeField] private float _playermaxSpeed = 5f;
        private float _sprintfactor = 2f;
        private float storePlayerSpeed;
        [Header("Rotation")] private  float horizontal;
        private  float vertical;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float rotationSmoothing = 10f;
        // private Vector3 direction = new Vector3(horizontal, vertical, 0f);

        #endregion

        #region Functions

        private void PlayerMovementUpdate()
        {
            
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * _playermaxSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(-Vector3.forward * _playermaxSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * _playermaxSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-Vector3.right * _playermaxSpeed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _playermaxSpeed *= _sprintfactor;
            }
            else
            {
                _playermaxSpeed = storePlayerSpeed;
            }
        }

        private void PlayerRotationUpdate()
        {
            horizontal = Input.GetAxis("Mouse X")* rotationSmoothing;
            vertical = Input.GetAxis("Mouse Y") * speed;
            Debug.Log(horizontal);
            horizontal = Mathf.Clamp(1, -90f, 90f);
            

            transform.Rotate(vertical, horizontal, 0f);
        }

        #endregion
        
        // float mouseX = Input.GetAxis("Mouse X");
        // // Debug.Log(mouseX);
        // horizontal = Input.GetAxis("Horizontal");
        // vertical = Input.GetAxis("Vertical");
        // Debug.Log(" Horizontal " + horizontal + " Vertical "+ vertical );
        // if (direction.magnitude >= 0.1f)
        // {
        //     Quaternion targetRotation = Quaternion.LookRotation(direction);
        //     transform.rotation =
        //         Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothing * Time.deltaTime);
        // }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            storePlayerSpeed = _playermaxSpeed;
            
        }

        // Update is called once per frame
        void Update()
        {
            PlayerMovementUpdate();
            PlayerRotationUpdate();
        }
    }
}