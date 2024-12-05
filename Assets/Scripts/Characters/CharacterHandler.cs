using Level;
using UnityEngine;

namespace Characters
{
    public class CharacterHandler : MonoBehaviour
    {
        public static CharacterHandler Instance { get; private set; }
        public delegate void CharacterControlsDelegate(CharacterControls controls);
        public event CharacterControlsDelegate OnFixedUpdate;
        public event CharacterControlsDelegate OnUpdate;

        private CharacterBase selectedCharacter;
        private CharacterControls controls;

        private void Awake()
        {
            Instance = this;
            controls = new CharacterControls();

            LevelData.OnLevelLoaded += LevelData_OnLevelLoaded;
            LevelData.OnLevelUnloaded += LevelData_OnLevelUnloaded;
            CharacterBase.OnCharacterSelected += CharacterBase_OnCharacterSelected;
            CharacterBase.OnCharacterDeselected += CharacterBase_OnCharacterDeselected;

            DontDestroyOnLoad(gameObject);
            LevelData.ReturnToMenu();
        }

        private void Update()
        {
            if (CharacterBase.ALL_CHARACTERS.Count > 0)
            {
                //Select the next character.
                if (Input.GetButtonDown("NextCharacter"))
                {
                    int index = (CharacterBase.ALL_CHARACTERS.IndexOf(selectedCharacter) + 1) % CharacterBase.ALL_CHARACTERS.Count;
                    if (selectedCharacter != null)
                        selectedCharacter.Deselect();
                    CharacterBase.ALL_CHARACTERS[index].Select();
                }
                //Reset the map.
                if (Input.GetButtonDown("Reset"))
                {
                    LevelData.CurrentLevel.Load();
                    return;
                }

                //Get user inputs.
                controls.RetakeInputs();
                //Call the update event.
                OnUpdate?.Invoke(controls);
                //Update all the character animations.
                for (int i = 0; i < CharacterBase.ALL_CHARACTERS.Count; i++)
                    if (CharacterBase.ALL_CHARACTERS[i])
                        CharacterBase.ALL_CHARACTERS[i].OnConstantUpdate(controls);
            }
        }
        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke(controls);
        }

        public CharacterBase GetSelectedCharacter()
        {
            return selectedCharacter;
        }

        private void LevelData_OnLevelLoaded(LevelData lvl, bool reloading)
        {
            Physics2D.gravity = new Vector2(0f, -12f);
            CameraHandler.Instace.enabled = true;
            Instance.enabled = true;

            CharacterBase.ALL_CHARACTERS[0].Select();
        }
        private void LevelData_OnLevelUnloaded(LevelData lvl, bool reloading)
        {
            CameraHandler.Instace.enabled = false;
            Instance.enabled = false;
        }
        private void CharacterBase_OnCharacterSelected(CharacterBase character)
        {
            if (selectedCharacter != character)
            {
                selectedCharacter?.Deselect();
                selectedCharacter = character;
            }
        }
        private void CharacterBase_OnCharacterDeselected(CharacterBase character)
        {
            if (selectedCharacter == character)
            {
                selectedCharacter = null;
            }
        }
    }
}
