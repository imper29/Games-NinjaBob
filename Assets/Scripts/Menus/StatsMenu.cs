using UnityEngine;
using UnityEngine.UI;

public class StatsMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject coinDisplay, clockDisplay;
    [SerializeField]
    private Text coinStats, clockStats;
    private Stats currentLevelStats;

    private void Awake()
    {
        coinDisplay.SetActive(false);
        clockDisplay.SetActive(false);
        Level.LevelData.OnLevelLoaded += LevelData_OnLevelLoaded;
        Level.LevelData.OnLevelUnloaded += LevelData_OnLevelUnloaded;
        Level.Collectables.CollectableCoin.OnCollectedEvent += CollectableCoin_OnCollectedEvent;
    }
    private void Update()
    {
        if (currentLevelStats != null)
        {
            currentLevelStats.timeElapsed += Time.deltaTime;

            int t = Mathf.FloorToInt(currentLevelStats.timeElapsed);
            clockStats.text = string.Format("x{0}:{1,2:D2}", t / 60, t % 60);
        }
    }

    private void LevelData_OnLevelLoaded(Level.LevelData lvl, bool reloading)
    {
        if (reloading)
            currentLevelStats.resets++;
        else
        {
            coinDisplay.SetActive(true);
            clockDisplay.SetActive(true);

            currentLevelStats = new Stats(lvl.levelName, 0f, 0, 0);
        }
        currentLevelStats.coinsCollected = 0;
        clockStats.text = string.Format("x0:00");
        coinStats.text = 'x' + currentLevelStats.coinsCollected.ToString();
    }
    private void LevelData_OnLevelUnloaded(Level.LevelData lvl, bool reloading)
    {
        if (!reloading)
        {
            coinDisplay.SetActive(false);
            clockDisplay.SetActive(false);
            currentLevelStats = null;
        }
    }

    private void CollectableCoin_OnCollectedEvent(Level.Collectables.CollectableCoin collectable, int totalCollected)
    {
        currentLevelStats.coinsCollected++;
        coinStats.text = 'x' + currentLevelStats.coinsCollected.ToString();
    }
}
public class Stats
{
    public string levelName;
    public float timeElapsed;
    public int coinsCollected;
    public int resets;

    public Stats(string levelName, float timeElapsed, int coinsCollected, int resets)
    {
        this.levelName = levelName;
        this.timeElapsed = timeElapsed;
        this.coinsCollected = coinsCollected;
        this.resets = resets;
    }
}