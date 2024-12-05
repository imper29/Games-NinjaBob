using UnityEngine;

namespace Characters
{
    [CreateAssetMenu]
    public class CharacterData : ScriptableObject
    {
        public Sprite sprite;
        public Color primaryColor, secondaryColor;

        public GameObject prefab;
    }
}
