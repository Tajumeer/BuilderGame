using UnityEngine;
using UnityEngine.EventSystems;

namespace Building
{
    public class BuildingManager : MonoBehaviour
    {
        public static BuildingManager Instance { get; private set; }

        public LayerMask Mask;
        private Vector3 _mousePos;
        private Camera _cam;
        private BuildingTypeSO _activeBuildingType;
        private BuildingTypeListSO _buildingTypeList;

        private void Awake()
        {
            Instance = this;
            _buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
            _activeBuildingType = null;

        }
        private void Start()
        {
            _cam = Camera.main;

        }



        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (_activeBuildingType != null)
                    Instantiate(_activeBuildingType.Prefab, GetMouseWorldPosition(), Quaternion.identity);
            }
        }

        private Vector3 GetMouseWorldPosition()
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000, Mask))
            {
                _mousePos = hit.point;
            }

            return _mousePos;

        }

        public void SetActiveBuildingType(BuildingTypeSO buildingType)
        {
            _activeBuildingType = buildingType;
        }

        public BuildingTypeSO GetActiveBuildingType()
        {
            return _activeBuildingType;
        }
    }
}

