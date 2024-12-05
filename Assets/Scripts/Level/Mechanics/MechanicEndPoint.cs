#pragma warning disable
using Characters;
using UnityEngine;
using UnityEngine.UI;

namespace Level.Mechanics
{
    [ExecuteAlways]
    public class MechanicEndPoint : MonoBehaviour
    {
        const float PROGRESS_DURATION = 1f / 3f;

        [SerializeField]
        private SpriteRenderer characterSpriteRenderer, padSpriteRenderer;
        public CharacterData character;
        private bool success;

        [ExecuteAlways]
        private void Awake()
        {
            characterSpriteRenderer.sprite = character.sprite;
            padSpriteRenderer.color = character.secondaryColor;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            CharacterBase c = collision.GetComponent<CharacterBase>();
            if (c && c.GetCharacterData() == character)
            {
                ProgressBar bar = c.GetEndProgressBar();
                bar.StartBar(PROGRESS_DURATION, TryCompleteLevel);
                bar.Progress = 0f;
                padSpriteRenderer.color = character.primaryColor;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            CharacterBase c = collision.GetComponent<CharacterBase>();
            if (c && c.GetCharacterData() == character)
            {
                ProgressBar bar = c.GetEndProgressBar();
                bar.StopBar();
                bar.Progress = 0f;
                padSpriteRenderer.color = character.secondaryColor;
            }
        }

        private void TryCompleteLevel()
        {
            success = true;
            MechanicEndPoint[] p = FindObjectsOfType<MechanicEndPoint>();
            for (int i = 0; i < p.Length; i++)
                if (!p[i].success)
                    return;

            if (LevelData.CurrentLevel.nextLevel)
                LevelData.CurrentLevel.nextLevel.Load();
            else
                LevelData.ReturnToMenu();
        }
    }
}
