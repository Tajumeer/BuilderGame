using UnityEngine;

namespace ResourcesScripts
{
    [System.Serializable]
    public class ResourceGeneratorData
    {
        public float TimerMax;
        public ResourceTypeSO ResourceType;
        public float ResourceDetectionRadius;
        public int MaxResourceAmount;
    }
}
