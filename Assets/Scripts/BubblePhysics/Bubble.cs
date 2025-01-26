using System;
using BubbleHell.Interfaces;
using UnityEngine;

namespace BubbleHell.BubblePhysics
{
    public class Bubble : MonoBehaviour, IBounceable
    {
        public static event Action OnBounce;

        [SerializeField] private float _maxVelocity;

        private Rigidbody _rb;

        private Vector3 _lastVelocity;
        public Vector3 Velocity => _lastVelocity;

        private LayerMask _layerMask;

        private void OnCollisionEnter(Collision collision)
        {
            IBounceable bounceable = collision.gameObject.GetComponentInParent<IBounceable>();
            if (bounceable != null || collision.transform.TryGetComponent(out bounceable))
            {
                bounceable.Hit(this);
            }

            if ((_layerMask & (1 << collision.gameObject.layer)) != 0)
            {
                OnBounce?.Invoke();
            }
        }

        private void Update()
        {
            _lastVelocity = _rb.linearVelocity;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _layerMask = ~(1 << LayerMask.NameToLayer("Ground"));
        }

        public void Hit(IBounceable bounceable)
        {
        }

        public void SetSpeed(float speed, Vector3 dir)
        {
            _rb.linearVelocity = Vector3.ClampMagnitude(dir * speed, _maxVelocity);
        }
    }
}
