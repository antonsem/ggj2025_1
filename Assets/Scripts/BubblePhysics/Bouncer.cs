using BubbleHell.Interfaces;
using UnityEngine;

namespace BubbleHell
{
    public class Bouncer : MonoBehaviour
    {
        [SerializeField] private float bounceForce = 10f;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out IBounceable bounceable))
            {
                bounceable.SetSpeed(bounceForce, CalculateBounceVector(bounceable.Velocity, collision.contacts[0].normal));
            }
        }

        private Vector3 CalculateBounceVector(Vector3 velocity, Vector3 collisionNormal)
        {
            float speed = velocity.magnitude;
            Vector3 direction = Vector3.Reflect(velocity.normalized, collisionNormal);

            return direction * speed;
        }
    }
}
