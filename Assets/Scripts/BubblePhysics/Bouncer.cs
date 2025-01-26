using BubbleHell.Interfaces;
using System;
using UnityEngine;

namespace BubbleHell.BubblePhysics
{
    public class Bouncer : MonoBehaviour
    {
        public static Action OnBounce;
        [SerializeField] private float bounceForce = 10f;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out IBounceable bounceable))
            {
                bounceable.SetSpeed(bounceForce, CalculateBounceVector(bounceable.Velocity, collision.contacts[0].normal));
                OnBounce?.Invoke();
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
