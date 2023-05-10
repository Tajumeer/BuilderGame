using System;
using Building;
using UnityEngine;

namespace ResourcesScripts
{
    public class ResourceGenerator : MonoBehaviour
    {
       
        private ResourceGeneratorData m_resourceGeneratorData;
        private float m_timer;
        private float m_timerMax;

        private void Awake()
        {
            m_resourceGeneratorData = GetComponent<BuildingTypeHolder>().BuildingType.ResourceGeneratorData;
            m_timerMax = m_resourceGeneratorData.TimerMax;
        }


        private void Start()
        {
           Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_resourceGeneratorData.ResourceDetectionRadius
                   );

           var nearbyResourceAmount = 0;
           foreach (Collider hitCollider in hitColliders)
           {
               ResourceNode resourceNode = hitCollider.GetComponent<ResourceNode>();
               if (resourceNode != null)
               {
                   nearbyResourceAmount++;
               }
           }

           nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, m_resourceGeneratorData.MaxResourceAmount);
           Debug.Log("nearbyResourceAmount: " + nearbyResourceAmount);
           
           //disable resource generation if no resource nodes are nearby
           if (nearbyResourceAmount == 0)
           {
               enabled = false;
           }
           else
           {
               m_timerMax = (m_resourceGeneratorData.TimerMax / 2f) + m_resourceGeneratorData.TimerMax * (1 - (float)nearbyResourceAmount / m_resourceGeneratorData.MaxResourceAmount);
           }
        }

        private void Update()
        {
            m_timer -= Time.deltaTime;
            if(m_timer <= 0f)
            {
                m_timer += m_timerMax;
             ResourceManager.Instance.AddResource(m_resourceGeneratorData.ResourceType, 1);  
            }
        }
    }
}
