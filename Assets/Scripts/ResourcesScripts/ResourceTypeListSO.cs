using System.Collections.Generic;
using UnityEngine;

namespace ResourcesScripts
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ResourceTypeList")]
    public class ResourceTypeListSO : ScriptableObject
    {
        public List<ResourceTypeSO> List;

    }
}
