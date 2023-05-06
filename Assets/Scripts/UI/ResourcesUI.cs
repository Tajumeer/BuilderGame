using System.Collections.Generic;
using ResourcesScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResourcesUI : MonoBehaviour
    {
        [SerializeField] private Transform m_resourceTemplate;
        private ResourceTypeListSO m_resourceTypeList;
        private Dictionary<ResourceTypeSO, Transform> m_resourceTypeTransformDict;
        private void Awake()
        {
            m_resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
            m_resourceTypeTransformDict = new Dictionary<ResourceTypeSO, Transform>();

            m_resourceTemplate.gameObject.SetActive(false);

            int index = 0;
            foreach (ResourceTypeSO resourceType in m_resourceTypeList.List)
            {
                Transform resourceTransform = Instantiate(m_resourceTemplate, transform);
                resourceTransform.gameObject.SetActive(true);

                float offsetAmount = -180f;
                resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100f + offsetAmount * index, -100f);

                resourceTransform.Find("Image").GetComponent<Image>().sprite = resourceType.Sprite;

                m_resourceTypeTransformDict[resourceType] = resourceTransform;
                index++;
            }
        }
        private void Start()
        {
            ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
            UpdateResourceAmount();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void ResourceManager_OnResourceAmountChanged(object _sender, System.EventArgs _e)
        {
            UpdateResourceAmount();
        }

        private void UpdateResourceAmount()
        {
            foreach (ResourceTypeSO resourceType in m_resourceTypeList.List)
            {
                Transform resourceTransform = m_resourceTypeTransformDict[resourceType];

                int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);

                resourceTransform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
            }
        }
    }
}
