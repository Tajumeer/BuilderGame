using UnityEngine;

namespace CameraControl
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;

        [SerializeField] private float _movementTime;
        [SerializeField] private float _fastSpeed;
        [SerializeField] private float _normalSpeed;
        [SerializeField] private float _rotationAmount;
        [SerializeField] private float _minZoom;
        [SerializeField] private float _maxZoom;

        private float _movementSpeed;

        private Vector3 _newZoom;
        private Vector3 _newPosition;
        private Quaternion _newRotation;
        private Camera _myCamera;

        private void Start()
        {
            _myCamera = Camera.main;
            Transform transformCache = transform;
            _newPosition = transformCache.position;
            _newRotation = transformCache.rotation;
            _newZoom = _cameraTransform.localPosition;
        }

        private void Update()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                _myCamera.fieldOfView = Mathf.Lerp(_myCamera.fieldOfView, _minZoom, .2f);
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                _myCamera.fieldOfView = Mathf.Lerp(_myCamera.fieldOfView, _maxZoom, .2f);
            }
        }

        private void LateUpdate()
        {
            HandleMovementInput();
        }

        private void HandleMovementInput()
        {
            _movementSpeed = Input.GetKey(KeyCode.LeftShift) ? _fastSpeed : _normalSpeed;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                _newPosition += (transform.forward * _movementSpeed);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                _newPosition -= (transform.right * _movementSpeed);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                _newPosition -= (transform.forward * _movementSpeed);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _newPosition += (transform.right * _movementSpeed);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                _newRotation *= Quaternion.Euler(Vector3.up * _rotationAmount);
            }

            if (Input.GetKey(KeyCode.E))
            {
                _newRotation *= Quaternion.Euler(Vector3.up * -_rotationAmount);
            }


            _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, _newZoom, Time.deltaTime * _movementTime);
            Transform transformCache;
            (transformCache = transform).position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * _movementTime);
            transform.rotation = Quaternion.Lerp(transformCache.rotation, _newRotation, Time.deltaTime * _movementTime);
        }
    }
}