using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Image))]
public class ArrowBobber : MonoBehaviour
{
    [SerializeField]
    public float bobAmount, bobDuration;
    [SerializeField]
    private AnimationCurve curve;

    private float bobState;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private RectTransform rectTransform;

    public Color Color
    {
        set
        {
            icon.color = value;
        }
    }
    private void Update()
    {
        bobState = (bobState + Time.deltaTime / bobDuration) % 2f;
        if (bobState > 1f)
            rectTransform.localPosition = new Vector3(0f, curve.Evaluate((2f - bobState)) * bobAmount, 0f);
        else
            rectTransform.localPosition = new Vector3(0f, curve.Evaluate(bobState) * bobAmount, 0f);
    }
}
