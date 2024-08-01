using UnityEngine;

namespace HlStudio
{
    public class CameraMovement : MonoBehaviour
    {
        public float moveSpeed = 10f;
        public float sprintMultiplier = 2f;
        public float rotationSmoothTime = 0.1f;

        public float mouseSensitivity = 100f;
        public bool invertY = false;

        private float rotationX = 0f;
        private float rotationY = 0f;
        private Vector3 currentRotation;
        private Vector3 rotationSmoothVelocity;
        
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (Application.isFocused)
            {
                HandleMovement();
                HandleMouseLook();
            }
        }

        private void HandleMovement()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            float speedMultiplier = Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f;

            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            transform.position += move * moveSpeed * speedMultiplier * Time.deltaTime;
        }

        private void HandleMouseLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            rotationY += mouseX;
            rotationX += invertY ? mouseY : -mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limit vertical rotation

            Vector3 targetRotation = new Vector3(rotationX, rotationY);
            currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref rotationSmoothVelocity,
                rotationSmoothTime);

            transform.eulerAngles = currentRotation;
        }
    }
}