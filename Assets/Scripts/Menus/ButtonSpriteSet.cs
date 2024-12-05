#pragma warning disable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ButtonSpriteSet : ScriptableObject
{
    [SerializeField]
    private Sprite NULL;
    [SerializeField]
    private ButtonRenderData[] buttons;

    public Sprite GetSprite(ButtonType button)
    {
        for (int i = 0; i < buttons.Length; i++)
            if (buttons[i].button == button)
                return buttons[i].sprite;

        return NULL;
    }

    [System.Serializable]
    private struct ButtonRenderData
    {
        public ButtonType button;
        public Sprite sprite;
    }
}

public enum ButtonType
{
    MoveHorizontal,
    MoveVertical,
    MoveBoth,
    Jump,
    Action
}
