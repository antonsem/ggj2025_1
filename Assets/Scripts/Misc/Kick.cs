using BubbleHell.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleHell
{
    public class Kick : MonoBehaviour
    {
        [SerializeField] private float kickForce;

        private bool _inRange;
        private List<IBounceable> _bounceables = new();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IBounceable bounceable))
            {
                _inRange = true;
                _bounceables.Add(bounceable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IBounceable bounceable))
            {
                _inRange = false;
                _bounceables.Remove(bounceable);
            }
        }

        private void Update()
        {
            if (_inRange)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    foreach (IBounceable bounceable in _bounceables)
                    {
                        bounceable.SetSpeed(kickForce, transform.forward);
                    }
                }
            }
        }
    }
}