using BubbleHell.Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace BubbleHell
{
    public class Bubble : MonoBehaviour, IBounceable
    {
        private PhysicsMaterial _physicsMaterial;
        private SphereCollider _sphereCollider;
        private Transform _startParent;
        private Rigidbody _rb;

        // For debug purposes.
        private Pickup _pickup;

        private void Start()
        {
            _startParent = transform.parent;
            _rb = GetComponent<Rigidbody>();
            _sphereCollider = GetComponent<SphereCollider>();
            _physicsMaterial = _sphereCollider.material;
            _sphereCollider.material = null;

            // For debug purposes.
            _pickup = FindObjectOfType<Pickup>();
        }

        public void Hit(IBounceable bounceable)
        {
            throw new System.NotImplementedException();
        }

        public void SetSpeed(float speed)
        {
            // For debug purposes.
            _rb.linearVelocity = Vector3.zero;
            _rb.isKinematic = speed == 0;
            _rb.AddForce(_pickup.transform.forward * speed);
        }

        public void SetParent([CanBeNull] Transform parent)
        {
            if (parent != null)
            {
                transform.SetParent(parent);
                transform.position = parent.position;
            }
            else transform.SetParent(_startParent);
        }

        public void Bounce(bool bounce)
        {
            _sphereCollider.material = bounce ? _physicsMaterial : null;
        }
    }
}