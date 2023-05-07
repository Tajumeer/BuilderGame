using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Building
{
    public class BuildingGhost : MonoBehaviour
    {
        [SerializeField] private GameObject m_woodGhost;
        [SerializeField] private GameObject m_stoneGhost;
        [SerializeField] private GameObject m_goldGhost;
        

        private void Awake()
        {
            Hide();
        }

        private void Start()
        {
            BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
        }

        private void BuildingManager_OnActiveBuildingTypeChanged(object _sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs _eventArgs)
        {
            if (_eventArgs.ActiveBuildingType == null)
            {
                Hide();
            }
            else if (_eventArgs.ActiveBuildingType.GhostName == "WoodGhost")
            {
                Hide();
                Show(m_woodGhost);
            }
            else if (_eventArgs.ActiveBuildingType.GhostName == "StoneGhost")
            {
                Hide();
                Show(m_stoneGhost);
            }
            else if (_eventArgs.ActiveBuildingType.GhostName == "GoldGhost")
            {
                Hide();
                Show(m_goldGhost);
            }
        }

        private void Update()
        {
            transform.position = BuildingManager.Instance.GetMouseWorldPosition();
        }

        private void Show(GameObject _ghostGameObject)
        {
          _ghostGameObject.SetActive(true);
           

        }

        private void Hide()
        {
            m_woodGhost.SetActive(false);
            m_stoneGhost.SetActive(false);
            m_goldGhost.SetActive(false);
        }
    }
}
