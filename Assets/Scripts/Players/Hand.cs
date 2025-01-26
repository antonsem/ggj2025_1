using BubbleHell.BubblePhysics;
using BubbleHell.Interfaces;
using UnityEngine;

namespace BubbleHell.Players
{
	public class Hand : MonoBehaviour
	{
		[SerializeField] private Player _player;
		[SerializeField] private AttackTrigger _attackTrigger;
		[SerializeField] private Transform _handPosition;
		[SerializeField] private float _handRadius;
		[SerializeField] private LayerMask _hitMask;
		[SerializeField] private float _hitForce = 25;
		[SerializeField] private ParticleSystem _hitEffect;

		private readonly RaycastHit[] _hit = new RaycastHit[1];
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
			_hitEffect.Play();

			int count = Physics.SphereCastNonAlloc(_handPosition.position, _handRadius, _handPosition.forward, _hit,
				0.1f, _hitMask);

			if(count > 0)
			{
				IBounceable bounceable = _hit[0].transform.GetComponentInParent<IBounceable>();
				if(bounceable != null || _hit[0].transform.TryGetComponent(out bounceable))
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
