using Characters;
using UnityEngine;

namespace Level.Mechanics
{
    public class MechanicDeath : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            CharacterBase c = collision.GetComponent<CharacterBase>();
            if (c)
            {
                Level.LevelData.CurrentLevel.Load();
            }
        }
    }
}
