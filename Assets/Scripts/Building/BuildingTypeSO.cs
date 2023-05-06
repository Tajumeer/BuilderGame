using ResourcesScripts;
using UnityEngine;

namespace Building
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
    public class BuildingTypeSO : ScriptableObject
    {
        public string NameString { get; }
        public Transform Prefab;
        public ResourceGeneratorData ResourceGeneratorData;
        public Sprite Sprite;
    }
}
