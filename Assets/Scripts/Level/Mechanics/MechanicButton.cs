using Characters;
using UnityEngine;
using static UnityEngine.UI.Button;

namespace Level.Mechanics
{
    public class MechanicButton : MonoBehaviour
    {
        [SerializeField]
        private CharacterData requiredCharacter;
        public CharacterData RequiredCharacter { get => requiredCharacter; set => requiredCharacter = value; }
        [SerializeField]
        private SpriteRenderer characterSpriteRenderer, buttonSpriteRenderer;
        [SerializeField]
        private Sprite onSprite, offSprite;
        [SerializeField]
        private ButtonClickedEvent onPressed, whilePressed, onReleased;

        private CharacterBase characterLastPressed;
        private bool isPressed;
        public bool IsPressed
        {
            get
            {
                return isPressed;
            }
            set
            {
                if (isPressed != value)
                {
                    isPressed = value;
                    if (value)
                    {
                        buttonSpriteRenderer.sprite = onSprite;
                        if (requiredCharacter)
                            buttonSpriteRenderer.color = requiredCharacter.primaryColor;
                        else
                            buttonSpriteRenderer.color = Color.white;
                        onPressed.Invoke();
                    }
                    else
                    {
                        buttonSpriteRenderer.sprite = offSprite;
                        if (requiredCharacter)
                            buttonSpriteRenderer.color = requiredCharacter.secondaryColor;
                        else
                            buttonSpriteRenderer.color = Color.gray;
                        onReleased.Invoke();
                    }
                }
                else if (isPressed)
                    whilePressed.Invoke();
            }
        }

        private void Awake()
        {
            buttonSpriteRenderer.sprite = offSprite;
            SetRequiredCharacter(requiredCharacter);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            CharacterBase c = collision.GetComponent<CharacterBase>();
            if (c)
            {
                if (c.GetCharacterData() == requiredCharacter || requiredCharacter == null)
                {
                    characterLastPressed = c;
                    IsPressed = true;
                }
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            CharacterBase c = collision.GetComponent<CharacterBase>();
            if (c)
            {
                if (c.GetCharacterData() == requiredCharacter || requiredCharacter == null)
                {
                    characterLastPressed = c;
                    IsPressed = true;
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            CharacterBase c = collision.GetComponent<CharacterBase>();
            if (c)
                if (c.GetCharacterData() == requiredCharacter || requiredCharacter == null)
                    IsPressed = false;
        }

        public CharacterBase GetActivator()
        {
            return characterLastPressed;
        }

        public void SetRequiredCharacter(CharacterData characterData)
        {
            this.requiredCharacter = characterData;
            if (IsPressed && characterLastPressed != characterData)
            {
                IsPressed = false;
            }
            if (characterData)
            {
                characterSpriteRenderer.sprite = characterData.sprite;
                buttonSpriteRenderer.color = IsPressed ? characterData.primaryColor : characterData.secondaryColor;
            }
            else
            {
                buttonSpriteRenderer.color = IsPressed ? Color.white : Color.gray;
            }
        }
    }
}
