using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GroundDetector : MonoBehaviour
    {
        public delegate void ImpactDelegate(Direction direction, Collider2D collision);
        public event ImpactDelegate OnImpact;

        private Rigidbody2D body;
        private Collider2D groundCollider;

        public Direction CollisionDirections { get; private set; }
        public bool IsGrounded { get; private set; }
        public Direction DownDirection
        {
            get
            {
                return body.gravityScale * Physics2D.gravity.y > 0f ? Direction.Up : Direction.Down;
            }
        }
        public Direction UpDirection
        {
            get
            {
                return body.gravityScale * Physics2D.gravity.y > 0f ? Direction.Down : Direction.Up;
            }
        }

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            CollisionDirections = Direction.None;
        }

        public void ColliderEnter(Direction direction, Collider2D collision)
        {
            CollisionDirections |= direction;

            if (direction == DownDirection)
            {
                IsGrounded = true;
                groundCollider = collision;
            }

            OnImpact?.Invoke(direction, collision);
        }
        public void ColliderStay(Direction direction, Collider2D collision)
        {
            CollisionDirections |= direction;

            if (direction == DownDirection)
            {
                IsGrounded = true;
                groundCollider = collision;
            }
        }
        public void ColliderExit(Direction direction)
        {
            CollisionDirections &= ~direction;

            if (direction == DownDirection)
            {
                IsGrounded = false;
                groundCollider = null;
            }
        }

        public Vector2 GetContactNormal()
        {
            return -DirectionHelper.GetVector(CollisionDirections).normalized;
        }
        public Vector2 GetGroundNormal()
        {
            return -DirectionHelper.GetVector(DownDirection);
        }
        public Collider2D GetGroundCollider()
        {
            return groundCollider;
        }
    }
}
