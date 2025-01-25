using BubbleHell.Movables;
using UnityEngine;

namespace BubbleHell.Players
{
	public class PlayerAnimator : MonoBehaviour
	{
		[SerializeField] private Transform _visuals;
		[SerializeField] private Movable _movable;
		[SerializeField] private float _rotationSpeed;

		[SerializeField] private Animator _animator;

		private readonly int _speedHash = Animator.StringToHash("Speed");

		#region Unity Methods

		private void Awake()
		{
			if(!_visuals)
			{
				Debug.LogError($"Visuals of {name} are not set! Will not animate...", this);
				enabled = false;
			}

			if(!_movable)
			{
				Debug.LogError($"Movable of {name} are not set! Will not animate...", this);
				enabled = false;
			}
		}

		private void Update()
		{
			if(_movable.DesiredVelocity.sqrMagnitude > 0.1f)
			{
				Vector3 desiredVelocity = _movable.DesiredVelocity;
				if(Vector3.Dot(_visuals.forward, _movable.DesiredVelocity) < -0.99f)
				{
					Vector3 delta = Vector3.Cross(desiredVelocity, Vector3.up) *
					                (Random.Range(0, 1) < .5f
						                ? -0.1f
						                : 0.1f);

					desiredVelocity += delta;
				}

				_visuals.forward = Vector3.Lerp(_visuals.forward, desiredVelocity, Time.deltaTime * _rotationSpeed);
			}

			SetAnimation();
		}

		#endregion

		private void SetAnimation()
		{
			float speed = _movable.DesiredVelocity.magnitude;
			_animator.SetFloat(_speedHash, speed);
		}
	}
}
