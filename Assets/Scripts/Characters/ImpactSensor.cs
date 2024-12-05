#pragma warning disable
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Collider2D))]
    public class ImpactSensor : MonoBehaviour
    {
        private GroundDetector groundDetector;
        [SerializeField]
        private Direction sensorDirection;

        private void Awake()
        {
            groundDetector = GetComponentInParent<GroundDetector>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.isTrigger)
                groundDetector.ColliderEnter(sensorDirection, collision);
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!collision.isTrigger)
                groundDetector.ColliderStay(sensorDirection, collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.isTrigger)
                groundDetector.ColliderExit(sensorDirection);
        }
    }
}
