using System.Collections.Generic;
using Building;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuildingTypeSelectUI : MonoBehaviour
    {
        [SerializeField] private Sprite _arrowSprite;
        [SerializeField] private Transform _buttonTemplate;

        private Dictionary<BuildingTypeSO, Transform> _buttonTransformDictionary;
        private Transform _arrowButton;
        [SerializeField] private float _uiPlacement = 262.5f;

        private void Awake()
        {
            _buttonTemplate.gameObject.SetActive(false);

            BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

            _buttonTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

            int index = 0;

            _arrowButton = Instantiate(_buttonTemplate, transform);
            _arrowButton.gameObject.SetActive(true);

            float offsetAmount = 175;

            _arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-_uiPlacement + offsetAmount * index, 100);

            _arrowButton.Find("image").GetComponent<Image>().sprite = _arrowSprite;

            _arrowButton.GetComponent<Button>().onClick.AddListener(() => { BuildingManager.Instance.SetActiveBuildingType(null); });

            index++;

            foreach (BuildingTypeSO buildingType in buildingTypeList.List)
            {
                Transform buttonTransform = Instantiate(_buttonTemplate, transform);
                buttonTransform.gameObject.SetActive(true);

                buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-_uiPlacement + offsetAmount * index, 100);

                buttonTransform.Find("image").GetComponent<Image>().sprite = buildingType.Sprite;

                buttonTransform.GetComponent<Button>().onClick.AddListener(() => { BuildingManager.Instance.SetActiveBuildingType(buildingType); });

                _buttonTransformDictionary[buildingType] = buttonTransform;

                index++;
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void Update()
        {
            UpdateActiveBuildingType();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void UpdateActiveBuildingType()
        {
            _arrowButton.Find("background").GetComponent<Outline>().enabled = false;
            foreach (BuildingTypeSO buildingType in _buttonTransformDictionary.Keys)
            {
                Transform buttonTransform = _buttonTransformDictionary[buildingType];
                buttonTransform.Find("background").GetComponent<Outline>().enabled = false;
            }

            BuildingTypeSO activeBuildingTyp = BuildingManager.Instance.GetActiveBuildingType();
            if (activeBuildingTyp == null)
            {
                _arrowButton.Find("background").GetComponent<Outline>().enabled = true;
            }
            else
            {
                _buttonTransformDictionary[activeBuildingTyp].Find("background").GetComponent<Outline>().enabled = true;
            }
        }
    }
}