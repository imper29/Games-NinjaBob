#pragma warning disable
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer)), ExecuteInEditMode]
public class ButtonSpriteRenderer : MonoBehaviour
{
    [SerializeField]
    private ButtonType button;
    [SerializeField]
    private ButtonSpriteSet sprites;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = sprites.GetSprite(button);
    }
}
