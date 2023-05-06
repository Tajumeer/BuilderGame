using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BuildingsTypeList")]
    public class BuildingTypeListSO : ScriptableObject
    {
        public List<BuildingTypeSO> List;
    }
}
