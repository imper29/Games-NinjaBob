#pragma warning disable
using UnityEngine;

namespace Characters
{
    public class CharacterBlue : CharacterBase
    {
        protected override void OnSelected()
        {
            CharacterHandler.Instance.OnFixedUpdate += Handler_OnFixedUpdate;
        }
        protected override void OnDeselected()
        {
            CharacterHandler.Instance.OnFixedUpdate -= Handler_OnFixedUpdate;
        }

        private void Handler_OnFixedUpdate(CharacterControls controls)
        {
            Axis2D motion = controls.Motion;
            DoMovement(controls);
            if (controls.Jump.pressed)
            {
                if (groundDetector.IsGrounded)
                {
                    PlayJumpSound();
                    Vector2 jumpForce = DirectionHelper.GetVector(groundDetector.UpDirection) * jumpStrength;
                    AddForce(jumpForce, ForceMode2D.Impulse, JUMP_RECOIL_FORCE_SCALE);
                }
                else
                {
                    Vector2 norm = groundDetector.GetContactNormal();
                    if (norm != Vector2.zero && norm != DirectionHelper.GetVector(groundDetector.DownDirection))
                    {
                        PlayJumpSound();
                        Velocity = new Vector2(Velocity.x, 0f);
                        Vector2 jumpForce = (groundDetector.GetContactNormal() + Vector2.up * 3f).normalized * jumpStrength;
                        AddForce(jumpForce, ForceMode2D.Impulse, JUMP_RECOIL_FORCE_SCALE);
                    }
                }
            }
        }
    }
}
