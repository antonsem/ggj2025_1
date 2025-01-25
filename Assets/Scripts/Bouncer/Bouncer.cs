using BubbleHell.Interfaces;
using UnityEngine;

namespace BubbleHell
{
    public class Bouncer : MonoBehaviour
    {
        [SerializeField]
        private float minVelocity = 10f;

        private Vector3 lastFrameVelocity;
        private Rigidbody rb;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IBounceable bounceable))
            {
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IBounceable bounceable))
            {
            }
        }

        private void OnEnable()
        {
            rb = GetComponent<Rigidbody>();
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
            var speed = lastFrameVelocity.magnitude;
            var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

            rb.linearVelocity = direction * Mathf.Max(speed, minVelocity);
        }
    }
}
