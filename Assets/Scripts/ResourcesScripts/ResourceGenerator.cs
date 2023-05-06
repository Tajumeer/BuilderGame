using Building;
using UnityEngine;

namespace ResourcesScripts
{
    public class ResourceGenerator : MonoBehaviour
    {
        private BuildingTypeSO _buildingType;
        private float _timer;
        private float _timerMax;

        private void Awake()
        {
            _buildingType= GetComponent<BuildingTypeHolder>().BuildingType;
            _timerMax = _buildingType.ResourceGeneratorData.TimerMax;
        }
        private void Update()
        {
            _timer -= Time.deltaTime;
            if(_timer <= 0f)
            {
                _timer += _timerMax;
                ResourceManager.Instance.AddResource(_buildingType.ResourceGeneratorData.ResourceType, 1);
            }
        }
    }
}
