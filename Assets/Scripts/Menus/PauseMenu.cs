#pragma warning disable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Vector2 showPosition, hidePosition;
    [SerializeField]
    private float transitionDuration;
    [SerializeField]
    private AnimationCurve showCurve;

    private float showAmount;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Level.LevelData.OnLevelLoaded += LevelData_OnLevelLoaded;
        Level.LevelData.OnLevelUnloaded += LevelData_OnLevelUnloaded;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    private void LevelData_OnLevelLoaded(Level.LevelData lvl, bool reloading)
    {
        enabled = true;
        transform.localPosition = hidePosition;
        showAmount = 0f;
    }
    private void LevelData_OnLevelUnloaded(Level.LevelData lvl, bool reloading)
    {
        enabled = false;
        transform.localPosition = hidePosition;
        showAmount = 0f;
    }

    public void Pause()
    {
        if (FindObjectOfType<Characters.CharacterBase>())
        {
            StopAllCoroutines();
            StartCoroutine(OnPause());
        }
    }
    public void Resume()
    {
        StopAllCoroutines();
        StartCoroutine(OnResume());
    }
    public void Restart()
    {
        Level.LevelData.CurrentLevel.Load();
    }
    public void Exit()
    {
        Level.LevelData.ReturnToMenu();
    }

    private IEnumerator OnPause()
    {
        while (showAmount < 1f)
        {
            showAmount += Time.deltaTime / transitionDuration;
            transform.localPosition = Vector3.Lerp(hidePosition, showPosition, showCurve.Evaluate(showAmount));
            yield return null;
        }
        showAmount = 1f;
        transform.localPosition = showPosition;
    }
    private IEnumerator OnResume()
    {
        while (showAmount > 0f)
        {
            showAmount -= Time.deltaTime / transitionDuration;
            transform.localPosition = Vector3.Lerp(hidePosition, showPosition, showCurve.Evaluate(showAmount));
            yield return null;
        }
        showAmount = 0f;
        transform.localPosition = hidePosition;
    }
}
