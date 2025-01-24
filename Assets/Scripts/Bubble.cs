using BubbleHell.Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace BubbleHell
{
    public class Bubble : MonoBehaviour, IBounceable
    {
        private Transform _startParent;
        private Rigidbody _rb;


        private void Start()
        {
            _startParent = transform.parent;
            _rb = GetComponent<Rigidbody>();

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
    }
}