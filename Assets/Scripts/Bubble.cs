using BubbleHell.Interfaces;
using UnityEngine;

namespace BubbleHell
{
    public class Bubble : MonoBehaviour, IBounceable
    {
        private Rigidbody _rb;

        private Vector3 _lastVelocity;
        public Vector3 Velocity => _lastVelocity;

        private void Update()
        {
            _lastVelocity = _rb.linearVelocity;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Hit(IBounceable bounceable)
        {
            throw new System.NotImplementedException();
        }

        public void SetSpeed(float speed, Vector3 dir)
        {
            _rb.linearVelocity = dir * speed;
        }
    }
}