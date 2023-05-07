using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace Building
{
    public class BuildingManager : MonoBehaviour
    {
        public static BuildingManager Instance { get; private set; }

        public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

        public class OnActiveBuildingTypeChangedEventArgs : EventArgs
        {
            public BuildingTypeSO ActiveBuildingType;
        }

        public LayerMask Mask;
        private Vector3 m_mousePos;
        private Camera m_cam;
        private BuildingTypeSO m_activeBuildingType;
        private BuildingTypeListSO m_buildingTypeList;

        private void Awake()
        {
            Instance = this;
            m_buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
            m_activeBuildingType = null;
        }

        private void Start()
        {
            m_cam = Camera.main;
        }


        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (m_activeBuildingType != null)
                    Instantiate(m_activeBuildingType.Prefab, GetMouseWorldPosition(), Quaternion.identity);
            }
        }

        public Vector3 GetMouseWorldPosition()
        {
            Ray ray = m_cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000, Mask))
            {
                m_mousePos = hit.point;
            }

            return m_mousePos;
        }

        public void SetActiveBuildingType(BuildingTypeSO _buildingType)
        {
            m_activeBuildingType = _buildingType;
            OnActiveBuildingTypeChanged?.Invoke(this,
                new OnActiveBuildingTypeChangedEventArgs { ActiveBuildingType = m_activeBuildingType });
        }

        public BuildingTypeSO GetActiveBuildingType()
        {
            return m_activeBuildingType;
        }
    }
}