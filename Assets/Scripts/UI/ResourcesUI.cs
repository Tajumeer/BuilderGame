using System.Collections.Generic;
using ResourcesScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResourcesUI : MonoBehaviour
    {
        [SerializeField] private Transform _resourceTemplate;
        private ResourceTypeListSO _resourceTypeList;
        private Dictionary<ResourceTypeSO, Transform> _resourceTypeTransformDict;
        private void Awake()
        {
            _resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
            _resourceTypeTransformDict = new Dictionary<ResourceTypeSO, Transform>();

            _resourceTemplate.gameObject.SetActive(false);

            int index = 0;
            foreach (ResourceTypeSO resourceType in _resourceTypeList.List)
            {
                Transform resourceTransform = Instantiate(_resourceTemplate, transform);
                resourceTransform.gameObject.SetActive(true);

                float offsetAmount = -180f;
                resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100f + offsetAmount * index, -100f);

                resourceTransform.Find("Image").GetComponent<Image>().sprite = resourceType.Sprite;

                _resourceTypeTransformDict[resourceType] = resourceTransform;
                index++;
            }
        }
        private void Start()
        {
            ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
            UpdateResourceAmount();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
        {
            UpdateResourceAmount();
        }

        private void UpdateResourceAmount()
        {
            foreach (ResourceTypeSO resourceType in _resourceTypeList.List)
            {
                Transform resourceTransform = _resourceTypeTransformDict[resourceType];

                int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);

                resourceTransform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
            }
        }
    }
}
