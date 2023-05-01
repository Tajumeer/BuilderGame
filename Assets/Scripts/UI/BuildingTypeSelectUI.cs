using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Transform buttonTemplate;

    private void Awake()
    {
        buttonTemplate.gameObject.SetActive(false);

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        int index = 0;
        foreach (BuildingTypeSO buildingType in buildingTypeList.list)
        {
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);

            float offsetAmount = 210f;
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2 (offsetAmount * index, 100);

            buttonTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            buttonTransform.GetComponent<Button>().onClick.AddListener(() => { BuildingManager.Instance.SetActiveBuildingType(buildingType); });
            index++;
        }
    }

}
