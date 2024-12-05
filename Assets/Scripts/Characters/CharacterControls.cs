using UnityEngine;

namespace Characters
{
    public class CharacterControls
    {
        public Axis2D Motion { get; private set; }
        public Button Jump { get; private set; }
        public Button Action { get; private set; }

        public void RetakeInputs()
        {
            Motion = new Axis2D("Horizontal", "Vertical");
            Jump = new Button("Jump");
            Action = new Button("Action");
        }
    }
    public struct Axis2D
    {
        public readonly Vector2 rawValue;
        public readonly Vector2 value;
        public readonly Direction direction;

        public Axis2D(string horizontalInputName, string verticalInputName)
        {
            rawValue = new Vector2(Input.GetAxisRaw(horizontalInputName), Input.GetAxisRaw(verticalInputName));
            value = new Vector2(Input.GetAxis(horizontalInputName), Input.GetAxis(verticalInputName));
            direction = DirectionHelper.GetDirection(value);
        }
    }
    public struct Button
    {
        public readonly bool pressed;
        public readonly bool firstFrame;
        public readonly bool lastFrame;

        public Button(string inputName) : this()
        {
            pressed = Input.GetButton(inputName);
            firstFrame = Input.GetButtonDown(inputName);
            lastFrame = Input.GetButtonUp(inputName);
        }
    }
}
