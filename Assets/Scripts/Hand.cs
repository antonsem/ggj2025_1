using BubbleHell.Interfaces;
using UnityEngine;

namespace BubbleHell
{
	public class Hand : MonoBehaviour
	{
		[SerializeField] private Transform _handPosition;
		[SerializeField] private float _handRadius;
		[SerializeField] private LayerMask _pickupMask;
		[SerializeField] private float _throwForce = 25;

		private readonly RaycastHit[] _hit = new RaycastHit[1];
		private int _workingFrames;
		private IBounceable _bubble;

		public void UseHand()
		{
			if(_bubble != null)
			{
				_bubble.SetSpeed(_throwForce, _handPosition.forward);
				return;
			}

			int count = Physics.SphereCastNonAlloc(_handPosition.position, _handRadius, _handPosition.forward, _hit,
				0.1f, _pickupMask);

			if(count > 0)
			{
				if(_hit[0].transform.TryGetComponent(out IBounceable bounceable))
				{
					// TODO: Check if bubble
					bounceable.SetSpeed(0);
					_bubble = bounceable;
				}
			}
		}
	}
}
