using PathCreation;
using UnityEngine;

[RequireComponent(typeof(PathCreator), typeof(Rigidbody2D))]
public class PlatformMovingPath : MonoBehaviour
{
    private PathCreator p;
    private Rigidbody2D body;
    [SerializeField]
    private float iterationsPerSecond;

    [SerializeField]
    private float lerp = 0f;

    private void Awake()
    {
        p = GetComponent<PathCreator>();
        body = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        lerp = (lerp + Time.deltaTime * iterationsPerSecond) % 1f;
        body.MovePosition(p.path.GetPoint(lerp, EndOfPathInstruction.Loop));
    }
}
