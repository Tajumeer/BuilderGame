using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float movementTime;
    [SerializeField] private float fastSpeed;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float rotationAmount;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;

    private float movementSpeed;

    [SerializeField] private Vector3 zoomAmount;
    private Vector3 newZoom;
    private Vector3 newPosition;
    private Quaternion newRotation;
    private Camera myCamera;

    private void Start()
    {
        myCamera = Camera.main;
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }
    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, minZoom, .2f);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, maxZoom, .2f);

        }
        
    }

    private void LateUpdate()
    {

        HandleMovementInput();
    }
    private void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition -= (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition -= (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }


        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
    }
}

