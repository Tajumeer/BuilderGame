using System;
using System.Collections.Generic;
using UnityEngine;

namespace ResourcesScripts
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager Instance { get; private set; }
        private Dictionary<ResourceTypeSO, int> _resourceAmountDictionary;

        public event EventHandler OnResourceAmountChanged;

        private void Awake()
        {
            Instance = this;

            _resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

            ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

            foreach (ResourceTypeSO resourceType in resourceTypeList.List)
            {
                _resourceAmountDictionary[resourceType] = 0;
            }
        }

        private void TestResourceAmountDict()
        {
            foreach(ResourceTypeSO resourceType in _resourceAmountDictionary.Keys)
            {
                Debug.Log(resourceType.name + ": " + _resourceAmountDictionary[resourceType]);
            }
        }

        public void AddResource(ResourceTypeSO resourceType, int amount)
        {
            _resourceAmountDictionary[resourceType] += amount;

            OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
        }

        public int GetResourceAmount(ResourceTypeSO resourceType)
        {
            return _resourceAmountDictionary[resourceType];
        }
    }
}
