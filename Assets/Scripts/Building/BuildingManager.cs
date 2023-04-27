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
    private BuildingTypeSO buildingType;
    private BuildingTypeListSO buildingTypeList;
    private void Awake()
    {
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        buildingType = buildingTypeList.list[0];

    }
    private void Start()
    {
        cam = Camera.main;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(buildingType.Prefab, GetMouseWorldPosition(), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            buildingType = buildingTypeList.list[0];
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            buildingType = buildingTypeList.list[1];
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

