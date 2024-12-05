using UnityEngine;

namespace Level.Obstacles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlatformMovingLinear : MonoBehaviour
    {
        private Rigidbody2D body;
        public Rigidbody2D Body { get => body; set => body = value; }

        [SerializeField]
        private MovementMode mode;
        public MovementMode Mode { get => mode; set => mode = value; }
        [SerializeField]
        private float cyclesPerSecond;
        public float CyclesPerSecond { get => cyclesPerSecond; set => cyclesPerSecond = value; }

        [SerializeField]
        private float lerp;
        public float PositionLerp
        {
            get
            {
                return lerp;
            }
            set
            {
                if (value > 1f)
                    body.MovePosition(Vector3.Lerp(positionA, positionB, 2f - value));
                else
                    body.MovePosition(Vector3.Lerp(positionA, positionB, value));
                lerp = value % 2f;
            }
        }

        [SerializeField]
        private Vector2 positionA, positionB;
        public Vector2 PositionA { get => positionA; set => positionA = value; }
        public Vector2 PositionB { get => positionB; set => positionB = value; }

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            float newLerp = lerp;
            if (Mode == MovementMode.Loop)
                newLerp = (lerp + Time.deltaTime * cyclesPerSecond) % 1f;
            else if (Mode == MovementMode.Circle)
                newLerp = (lerp + Time.deltaTime * cyclesPerSecond) % 2f;
            else if (Mode == MovementMode.ToA)
            {
                if (lerp > 0f)
                {
                    newLerp -= Time.deltaTime * cyclesPerSecond;
                    if (lerp < 0f)
                        newLerp = 0f;
                }
            }
            else if (Mode == MovementMode.ToB)
            {
                if (lerp < 1f)
                {
                    newLerp += Time.deltaTime * cyclesPerSecond;
                    if (lerp > 1f)
                        newLerp = 1f;
                }
            }
            PositionLerp = newLerp;
        }

        public enum MovementMode
        {
            ToA,
            ToB,
            Circle,
            Loop
        }
    }
}
