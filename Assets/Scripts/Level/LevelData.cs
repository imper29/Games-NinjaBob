#pragma warning disable
using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    [CreateAssetMenu]
    public class LevelData : ScriptableObject
    {
        public delegate void LevelDelegate(LevelData lvl, bool reloading = false);
        public static event LevelDelegate OnLevelLoaded;
        public static event LevelDelegate OnLevelUnloaded;

        public static LevelData CurrentLevel { get; private set; }
        public static float TimeSinceLevelStart
        {
            get
            {
                return Time.fixedTime - levelStartTime;
            }
        }
        private static float levelStartTime;

        public string levelName;
        public LevelData nextLevel;
        public Music music;

        public static void ReturnToMenu()
        {
            if (CurrentLevel)
            {
                OnLevelUnloaded?.Invoke(CurrentLevel);
                CurrentLevel = null;
            }
            SceneManager.LoadScene("Main Menu");
        }
        public void Load()
        {
            bool reloading = CurrentLevel == this;

            if (CurrentLevel)
                OnLevelUnloaded?.Invoke(CurrentLevel, reloading);

            SceneManager.LoadScene(name);

            CurrentLevel = this;

            levelStartTime = Time.fixedTime;
            CharacterHandler.Instance.StartCoroutine(PostLoad(reloading));
        }
        private IEnumerator PostLoad(bool reloading)
        {
            yield return null;
            OnLevelLoaded?.Invoke(CurrentLevel, reloading);
        }
    }
}
