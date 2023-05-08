using System;
using System.Collections.Generic;
using Building;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuildingTypeSelectUI : MonoBehaviour
    {
        [SerializeField] private Sprite m_arrowSprite;
        [SerializeField] private Transform m_buttonTemplate;

        private Dictionary<BuildingTypeSO, Transform> m_buttonTransformDictionary;
        private Transform m_arrowButton;
        [SerializeField] private float m_uiPlacement = 262.5f;

        private void Awake()
        {
            m_buttonTemplate.gameObject.SetActive(false);

            BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

            m_buttonTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

            int index = 0;

            m_arrowButton = Instantiate(m_buttonTemplate, transform);
            m_arrowButton.gameObject.SetActive(true);

            float offsetAmount = 175;

            m_arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-m_uiPlacement + offsetAmount * index, 100);

            m_arrowButton.Find("image").GetComponent<Image>().sprite = m_arrowSprite;

            m_arrowButton.GetComponent<Button>().onClick.AddListener(() => { BuildingManager.Instance.SetActiveBuildingType(null); });

            index++;

            foreach (BuildingTypeSO buildingType in buildingTypeList.List)
            {
                Transform buttonTransform = Instantiate(m_buttonTemplate, transform);
                buttonTransform.gameObject.SetActive(true);

                buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-m_uiPlacement + offsetAmount * index, 100);

                buttonTransform.Find("image").GetComponent<Image>().sprite = buildingType.Sprite;

                buttonTransform.GetComponent<Button>().onClick.AddListener(() => { BuildingManager.Instance.SetActiveBuildingType(buildingType); });

                m_buttonTransformDictionary[buildingType] = buttonTransform;

                index++;
            }
        }

        private void Start()
        {
            BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
            UpdateActiveBuildingType();
        }

        private void BuildingManager_OnActiveBuildingTypeChanged(object _sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs _eventArgs)
        {
            UpdateActiveBuildingType();
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private void UpdateActiveBuildingType()
        {
            m_arrowButton.Find("background").GetComponent<Outline>().enabled = false;
            foreach (BuildingTypeSO buildingType in m_buttonTransformDictionary.Keys)
            {
                Transform buttonTransform = m_buttonTransformDictionary[buildingType];
                buttonTransform.Find("background").GetComponent<Outline>().enabled = false;
            }

            BuildingTypeSO activeBuildingTyp = BuildingManager.Instance.GetActiveBuildingType();
            if (activeBuildingTyp == null)
            {
                m_arrowButton.Find("background").GetComponent<Outline>().enabled = true;
            }
            else
            {
                m_buttonTransformDictionary[activeBuildingTyp].Find("background").GetComponent<Outline>().enabled = true;
            }
        }
    }
}