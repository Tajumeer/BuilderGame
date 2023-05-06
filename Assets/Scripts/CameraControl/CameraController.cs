using UnityEngine;

namespace CameraControl
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform m_cameraTransform;

        [SerializeField] private float m_movementTime;
        [SerializeField] private float m_fastSpeed;
        [SerializeField] private float m_normalSpeed;
        [SerializeField] private float m_rotationAmount;
        [SerializeField] private float m_minZoom;
        [SerializeField] private float m_maxZoom;

        private float m_movementSpeed;

        private Vector3 m_newZoom;
        private Vector3 m_newPosition;
        private Quaternion m_newRotation;
        private Camera m_myCamera;

        private void Start()
        {
            m_myCamera = Camera.main;
            Transform transformCache = transform;
            m_newPosition = transformCache.position;
            m_newRotation = transformCache.rotation;
            m_newZoom = m_cameraTransform.localPosition;
        }

        private void Update()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                m_myCamera.fieldOfView = Mathf.Lerp(m_myCamera.fieldOfView, m_minZoom, .2f);
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                m_myCamera.fieldOfView = Mathf.Lerp(m_myCamera.fieldOfView, m_maxZoom, .2f);
            }
        }

        private void LateUpdate()
        {
            HandleMovementInput();
        }

        private void HandleMovementInput()
        {
            m_movementSpeed = Input.GetKey(KeyCode.LeftShift) ? m_fastSpeed : m_normalSpeed;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                m_newPosition += (transform.forward * m_movementSpeed);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                m_newPosition -= (transform.right * m_movementSpeed);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                m_newPosition -= (transform.forward * m_movementSpeed);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                m_newPosition += (transform.right * m_movementSpeed);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                m_newRotation *= Quaternion.Euler(Vector3.up * m_rotationAmount);
            }

            if (Input.GetKey(KeyCode.E))
            {
                m_newRotation *= Quaternion.Euler(Vector3.up * -m_rotationAmount);
            }


            m_cameraTransform.localPosition = Vector3.Lerp(m_cameraTransform.localPosition, m_newZoom, Time.deltaTime * m_movementTime);
            Transform transformCache;
            (transformCache = transform).position = Vector3.Lerp(transform.position, m_newPosition, Time.deltaTime * m_movementTime);
            transform.rotation = Quaternion.Lerp(transformCache.rotation, m_newRotation, Time.deltaTime * m_movementTime);
        }
    }
}