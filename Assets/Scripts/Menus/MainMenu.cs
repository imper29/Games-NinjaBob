#pragma warning disable
using Level;
using UnityEngine;
using UnityEngine.UI;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Music music;

        [SerializeField]
        private Button buttonPrefab;
        [SerializeField]
        private Transform loadLevelPanel;

        private void Awake()
        {
            PopulateLoadLevelPanel();
            MusicHandler.Instance.PlayMusic(music);
        }

        private void PopulateLoadLevelPanel()
        {
            LevelData[] levels = Resources.LoadAll<LevelData>("Levels");
            for (int i = 0; i < levels.Length; i++)
            {
                LevelData l = levels[i];
                Button b = Instantiate(buttonPrefab, loadLevelPanel);
                b.onClick.AddListener(l.Load);
                b.GetComponentInChildren<Text>().text = l.levelName;
                b.gameObject.SetActive(true);
            }
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
