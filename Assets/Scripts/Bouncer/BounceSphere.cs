using BubbleHell.Interfaces;
using UnityEngine;

namespace BubbleHell
{
    public class BounceSphere : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (TryGetComponent(out IBounceable bounceable))
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 collisionNormal = collision.GetContact(0).normal;
                rb.linearVelocity = Vector3.Reflect(rb.linearVelocity, collisionNormal);
            }
        }
    }
}
