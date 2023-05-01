using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private Transform buttonTemplate;

    private Dictionary<BuildingTypeSO, Transform> buttonTransfromDictionary;
    private Transform arrowButton;
    private float uiPlacement = 262.5f;

    private void Awake()
    {
        buttonTemplate.gameObject.SetActive(false);

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        buttonTransfromDictionary = new Dictionary<BuildingTypeSO, Transform>();

        int index = 0;

        arrowButton = Instantiate(buttonTemplate, transform);
        arrowButton.gameObject.SetActive(true);

        float offsetAmount = 175;
            
        arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-uiPlacement + offsetAmount * index, 100);

        arrowButton.Find("image").GetComponent<Image>().sprite = arrowSprite;

        arrowButton.GetComponent<Button>().onClick.AddListener(() => { BuildingManager.Instance.SetActiveBuildingType(null); });

        index++;

        foreach (BuildingTypeSO buildingType in buildingTypeList.list)
        {
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);

            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-uiPlacement + offsetAmount * index, 100);

            buttonTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            buttonTransform.GetComponent<Button>().onClick.AddListener(() => { BuildingManager.Instance.SetActiveBuildingType(buildingType); });

            buttonTransfromDictionary[buildingType] = buttonTransform;

            index++;
        }
    }

    private void Update()
    {
        UpdateActiveBuildingType();

    }
    private void UpdateActiveBuildingType()
    {
        arrowButton.Find("background").GetComponent<Outline>().enabled = false;
        foreach (BuildingTypeSO buildingType in buttonTransfromDictionary.Keys)
        {
            Transform buttonTransform = buttonTransfromDictionary[buildingType];
            buttonTransform.Find("background").GetComponent<Outline>().enabled = false;
        }

        BuildingTypeSO activeBuildingTyp = BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuildingTyp == null)
        {
            arrowButton.Find("background").GetComponent<Outline>().enabled = true;
        }
        else
        {
            buttonTransfromDictionary[activeBuildingTyp].Find("background").GetComponent<Outline>().enabled = true;
        }
    }
}
