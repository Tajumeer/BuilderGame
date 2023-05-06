using System;
using System.Collections.Generic;
using UnityEngine;

namespace ResourcesScripts
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager Instance { get; private set; }
        private Dictionary<ResourceTypeSO, int> m_resourceAmountDictionary;

        public event EventHandler OnResourceAmountChanged;

        private void Awake()
        {
            Instance = this;

            m_resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

            ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

            foreach (ResourceTypeSO resourceType in resourceTypeList.List)
            {
                m_resourceAmountDictionary[resourceType] = 0;
            }
        }

        private void TestResourceAmountDict()
        {
            foreach(ResourceTypeSO resourceType in m_resourceAmountDictionary.Keys)
            {
                Debug.Log(resourceType.name + ": " + m_resourceAmountDictionary[resourceType]);
            }
        }

        public void AddResource(ResourceTypeSO _resourceType, int _amount)
        {
            m_resourceAmountDictionary[_resourceType] += _amount;

            OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
        }

        public int GetResourceAmount(ResourceTypeSO _resourceType)
        {
            return m_resourceAmountDictionary[_resourceType];
        }
    }
}
