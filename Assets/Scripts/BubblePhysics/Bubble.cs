using System;
using BubbleHell.Interfaces;
using UnityEngine;

namespace BubbleHell.BubblePhysics
{
	public class Bubble : MonoBehaviour, IBounceable
	{
		[SerializeField] private float _maxVelocity;

		private Rigidbody _rb;

		private Vector3 _lastVelocity;
		public Vector3 Velocity => _lastVelocity;

		private void OnCollisionEnter(Collision other)
		{
			IBounceable bounceable = other.gameObject.GetComponentInParent<IBounceable>();
			if(bounceable != null || other.transform.TryGetComponent(out bounceable))
			{
				bounceable.Hit(this);
			}
		}

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
		}

		public void SetSpeed(float speed, Vector3 dir)
		{
			_rb.linearVelocity = Vector3.ClampMagnitude(dir * speed, _maxVelocity);
		}
	}
}
