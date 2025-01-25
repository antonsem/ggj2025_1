using UnityEngine;

namespace BubbleHell
{
    public class Homing : MonoBehaviour
    {
        [SerializeField]
        [Range(0f, 1f)]
        [Tooltip("0 = regular bounce ignoring player | 1 = direct to the player")]
        private float bias = 0.5f;

        [SerializeField]
        private Transform playerTransform;

        [SerializeField]
        private float bounceVelocity = 10f;

        private Vector3 lastFrameVelocity;
        private Rigidbody rb;

        private void OnEnable()
        {
            rb = GetComponent<Rigidbody>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            lastFrameVelocity = rb.linearVelocity;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Bounce(collision.contacts[0].normal);
        }

        private void Bounce(Vector3 collisionNormal)
        {
            var bounceDirection = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);
            var directionToPlayer = playerTransform.position - transform.position;

            var direction = Vector3.Lerp(bounceDirection, directionToPlayer, bias);

            rb.linearVelocity = direction * bounceVelocity;
        }
    }
}
