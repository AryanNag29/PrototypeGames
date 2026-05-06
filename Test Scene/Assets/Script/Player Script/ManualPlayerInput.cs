using UnityEngine;

namespace PrototypeGames
{
    public class ManualPlayerInput : MonoBehaviour
    {
        #region Variables

        [Header("Movement")]
        [SerializeField] private float _playermaxSpeed = 5f;
        private float _currentSpeed;

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
        }

        #endregion


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            PlayerMovementUpdate();
        }
    }
}