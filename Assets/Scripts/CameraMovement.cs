using UnityEngine;

namespace HlStudio
{
    public class CameraMovement : MonoBehaviour
    {
        // Movement settings
        public float moveSpeed = 10f;
        public float sprintMultiplier = 2f;
        public float rotationSmoothTime = 0.1f;

        // Mouse settings
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

        void Update()
        {
            HandleMovement();
            HandleMouseLook();
        }

        void HandleMovement()
        {
            // Get input for movement
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            // Sprinting
            float speedMultiplier = Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f;

            // Apply movement
            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            transform.position += move * moveSpeed * speedMultiplier * Time.deltaTime;
        }

        void HandleMouseLook()
        {
            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Calculate rotation
            rotationY += mouseX;
            rotationX += invertY ? mouseY : -mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);  // Limit vertical rotation

            // Smooth rotation
            Vector3 targetRotation = new Vector3(rotationX, rotationY);
            currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref rotationSmoothVelocity, rotationSmoothTime);

            // Apply rotation
            transform.eulerAngles = currentRotation;
        }
    }
}