﻿using BubbleHell.BubblePhysics;
using BubbleHell.Interfaces;
using System;
using UnityEngine;

namespace BubbleHell.Players
{
	public class Hand : MonoBehaviour
	{
		public static event Action OnHandUsed;

		[SerializeField] private Player _player;
		[SerializeField] private AttackTrigger _attackTrigger;
		[SerializeField] private Transform _handPosition;
		[SerializeField] private float _handRadius;
		[SerializeField] private LayerMask _hitMask;
		[SerializeField] private float _hitForce = 25;
		[SerializeField] private float _hitDistance = 1;
		[SerializeField] private ParticleSystem _hitEffect;

		private readonly RaycastHit[] _hit = new RaycastHit[4];
		private int _workingFrames;

		private void Awake()
		{
			_attackTrigger.OnAttacked += UseHand;
		}

		private void OnDestroy()
		{
			_attackTrigger.OnAttacked -= UseHand;
		}

		private void UseHand()
		{
			OnHandUsed?.Invoke();
			_hitEffect.Play();

			int count = Physics.SphereCastNonAlloc(_handPosition.position, _handRadius, _handPosition.forward, _hit,
				_hitDistance, _hitMask);

			for (int i = 0; i < count; i++)
			{
				IBounceable bounceable = _hit[i].transform.GetComponentInParent<IBounceable>();
				if(bounceable != null || _hit[i].transform.TryGetComponent(out bounceable))
				{
					bounceable.Hit(_player);
					if(bounceable is Bubble)
					{
						bounceable.SetSpeed(_hitForce, _handPosition.forward);
					}
				}
			}
		}
	}
}
