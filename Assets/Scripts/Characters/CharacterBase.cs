#pragma warning disable
using Level;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Characters
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(GroundDetector))]
    public abstract class CharacterBase : MonoBehaviour
    {
        protected const float JUMP_RECOIL_FORCE_SCALE = 7f;

        public static readonly List<CharacterBase> ALL_CHARACTERS = new List<CharacterBase>();

        public delegate void CharacterDelegate(CharacterBase character);
        public static event CharacterDelegate OnCharacterSelected;
        public static event CharacterDelegate OnCharacterDeselected;

        [SerializeField]
        private CharacterData characterData;
        [SerializeField]
        private Transform renderingTransform;
        [SerializeField]
        private SoundFx jumpSound;
        private float jumpSoundTime = -2f;
        [SerializeField]
        protected float jumpStrength = 3.7f;
        [SerializeField]
        protected float moveSpeed = 6f;

        private ProgressBar endPointProgressBar;
        protected GroundDetector groundDetector;
        private Rigidbody2D body;
        protected Animator animator;
        protected ArrowBobber arrowBobber;

        public float GravityScale
        {
            get
            {
                return body.gravityScale;
            }
            set
            {
                body.gravityScale = value;
            }
        }
        public Vector2 Velocity
        {
            get
            {
                return body.velocity;
            }
            set
            {
                body.velocity = value;
            }
        }

        protected virtual void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            groundDetector = GetComponent<GroundDetector>();
            animator = GetComponent<Animator>();
            endPointProgressBar = GetComponentInChildren<ProgressBar>();
            arrowBobber = GetComponentInChildren<ArrowBobber>();
            arrowBobber.bobAmount = 0f;
            arrowBobber.Color = characterData.secondaryColor;

            endPointProgressBar.ForegroundColor = characterData.primaryColor;
            endPointProgressBar.BackgroundColor = characterData.secondaryColor;

            //Add this character to the list of all characters.
            ALL_CHARACTERS.Add(this);
        }
        private void OnDestroy()
        {
            ALL_CHARACTERS.Remove(this);
            Deselect();
        }

        public CharacterData GetCharacterData()
        {
            return characterData;
        }
        public ProgressBar GetEndProgressBar()
        {
            return endPointProgressBar;
        }
        public GroundDetector GetGroundDetector()
        {
            return groundDetector;
        }

        /// <summary>
        /// Called every frame from the CharacterHandler.
        /// </summary>
        /// <param name="controls">The input from the current frame.</param>
        public virtual void OnConstantUpdate(CharacterControls controls)
        {
            animator.SetFloat("HorizontalVelocity", body.velocity.x);
            animator.SetBool("InAir", !groundDetector.IsGrounded);

            if (CharacterHandler.Instance.GetSelectedCharacter() == this)
                animator.SetInteger("HorizontalMovement", (int)DirectionHelper.GetVector(controls.Motion.direction).x);
            else
                animator.SetInteger("HorizontalMovement", 0);

            if (GravityScale * Physics2D.gravity.y > 0f)
            {
                //Flip rendering.
                renderingTransform.localScale = new Vector3(1f, -1f, 1f);
            }
            else
            {
                //Normal rendering.
                renderingTransform.localScale = Vector3.one;
            }
        }
        /// <summary>
        /// Moves the character.
        /// </summary>
        /// <param name="controls">The input from the current frame.</param>
        protected void DoMovement(CharacterControls controls)
        {
            Vector2 velocity = body.velocity;
            Vector2 force = new Vector2(controls.Motion.value.x * (moveSpeed - Mathf.Abs(velocity.x)), 0f);

            if (groundDetector.IsGrounded)
                AddForce(force * 6f);
            else
                AddForce(force * 2f);
        }
        /// <summary>
        /// Makes the character jump.
        /// </summary>
        protected void DoJump()
        {
            Vector2 norm = groundDetector.GetGroundNormal();

            if (groundDetector.IsGrounded)
            {
                PlayJumpSound();
                AddForce(norm * jumpStrength, ForceMode2D.Impulse, JUMP_RECOIL_FORCE_SCALE);
            }
        }
        /// <summary>
        /// Plays the jump sound.
        /// </summary>
        protected void PlayJumpSound()
        {
            if (jumpSoundTime + 0.1f < Time.fixedTime)
            {
                jumpSoundTime = Time.fixedTime;
                SoundFxHandler.Instance.PlaySound(jumpSound, transform.position);
            }
        }

        protected void AddForce(Vector2 force, ForceMode2D mode = ForceMode2D.Force, float recoilScale = 3f)
        {
            body.AddForce(force, mode);
            Collider2D collider = groundDetector.GetGroundCollider();
            if (collider)
            {
                Rigidbody2D b = collider.GetComponent<Rigidbody2D>();
                if (b)
                {
                    b.AddForce(-force, mode);
                }
            }
        }


        public void Select()
        {
            OnSelected();

            arrowBobber.bobAmount = 0.3f;
            arrowBobber.Color = characterData.primaryColor;
            OnCharacterSelected?.Invoke(this);
        }
        public void Deselect()
        {
            OnDeselected();

            arrowBobber.bobAmount = 0f;
            Color c = characterData.primaryColor / 3f;
            c.a = 1f;
            arrowBobber.Color = c;
            OnCharacterDeselected?.Invoke(this);
        }

        protected abstract void OnSelected();
        protected abstract void OnDeselected();
    }
}
