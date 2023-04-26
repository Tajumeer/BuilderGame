using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class BuildingManager : MonoBehaviour
{
    public LayerMask mask;
    private Vector3 mousePos;
    private Camera cam;
    [SerializeField] private GameObject prefabWoodHarvester;
    private Transform target;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(prefabWoodHarvester, GetMouseWorldPosition(), Quaternion.identity);
      

        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, mask))
        {
            mousePos = hit.point;
        }

        return mousePos;

    }
}

