using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    public LayerMask mask;
    private Vector3 mousePos;
    private Camera cam;
    private BuildingTypeSO activeBuildingType;
    private BuildingTypeListSO buildingTypeList;

    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        activeBuildingType = null;

    }
    private void Start()
    {
        cam = Camera.main;

    }



    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingType != null)
                Instantiate(activeBuildingType.Prefab, GetMouseWorldPosition(), Quaternion.identity);
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

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }
}

