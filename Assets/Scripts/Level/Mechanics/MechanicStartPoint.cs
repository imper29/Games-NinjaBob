using Characters;
using UnityEngine;

namespace Level.Mechanics
{
    [ExecuteAlways]
    public class MechanicStartPoint : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer characterSpriteRenderer, padSpriteRenderer;
        public CharacterData character;

        [ExecuteAlways]
        private void Awake()
        {
            characterSpriteRenderer.sprite = character.sprite;
            padSpriteRenderer.color = character.primaryColor;
            if (Application.isPlaying)
                Instantiate(character.prefab, transform.position, Quaternion.identity);
        }
    }
}
