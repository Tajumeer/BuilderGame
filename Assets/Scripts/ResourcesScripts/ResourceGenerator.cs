using Building;
using UnityEngine;

namespace ResourcesScripts
{
    public class ResourceGenerator : MonoBehaviour
    {
        private BuildingTypeSO m_buildingType;
        private float m_timer;
        private float m_timerMax;

        private void Awake()
        {
            m_buildingType= GetComponent<BuildingTypeHolder>().BuildingType;
            m_timerMax = m_buildingType.ResourceGeneratorData.TimerMax;
        }
        private void Update()
        {
            m_timer -= Time.deltaTime;
            if(m_timer <= 0f)
            {
                m_timer += m_timerMax;
                ResourceManager.Instance.AddResource(m_buildingType.ResourceGeneratorData.ResourceType, 1);
            }
        }
    }
}
