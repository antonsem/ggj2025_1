using BubbleHell.Interfaces;
using UnityEngine;

namespace BubbleHell
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private Transform attachTransform;

        private bool _inPickupRange;
        private bool _isPicked;
        private IBounceable _bounceable;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IBounceable bounceable))
            {
                _inPickupRange = true;
                if (!_isPicked)
                {
                    _bounceable = bounceable;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IBounceable bounceable))
            {
                _inPickupRange = false;
                if (!_isPicked)
                {
                    _bounceable = null;
                }
            }
        }

        private void Update()
        {
            if (_inPickupRange)
            {
                if(Input.GetKeyDown(KeyCode.Space) && !_isPicked)
                {
                    _isPicked = true;
                    _bounceable.SetSpeed(0);
                    _bounceable.SetParent(transform);
                }

                if(Input.GetMouseButtonDown(0) && _isPicked)
                {
                    _bounceable.SetSpeed(1000);
                    _bounceable.SetParent(null);
                    _isPicked = false;
                }
            }
        }
    }
}