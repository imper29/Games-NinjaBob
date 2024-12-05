#pragma warning disable
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image backgroundImage, foregroundImage;
    [SerializeField]
    private RectTransform barMask;
    [SerializeField]
    private bool verticalBar, hideWhenEmpty;

    private float progress;
    public float Progress
    {
        get
        {
            return progress;
        }
        set
        {
            progress = value;

            if (verticalBar)
                barMask.localScale = new Vector3(1f, value);
            else
                barMask.localScale = new Vector3(value, 1f);

            if (hideWhenEmpty && value == 0f)
                backgroundImage.enabled = false;
            else
                backgroundImage.enabled = true;
        }
    }
    public Color BackgroundColor
    {
        get
        {
            return backgroundImage.color;
        }
        set
        {
            backgroundImage.color = value;
        }
    }
    public Color ForegroundColor
    {
        get
        {
            return foregroundImage.color;
        }
        set
        {
            foregroundImage.color = value;
        }
    }

    private void Awake()
    {
        Progress = 0f;
    }

    public void StartBar(float timeMultiplier, Action onFilled)
    {
        StopAllCoroutines();
        StartCoroutine(DoProgressBar(timeMultiplier, onFilled));
    }
    public void StopBar()
    {
        StopAllCoroutines();
    }

    private IEnumerator DoProgressBar(float timeMultiplier, Action onFilled)
    {
        while (progress < 1f)
        {
            Progress += Time.deltaTime * timeMultiplier;
            yield return null;
        }
        onFilled();
    }
}
