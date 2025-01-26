using BubbleHell.Misc;
using BubbleHell.Movables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BubbleHell.Players
{
	public class PlayerAnimator : MonoBehaviour
	{
		[SerializeField] private Transform _visuals;
		[SerializeField] private Player _player;
		[SerializeField] private Movable _movable;
		[SerializeField] private float _rotationSpeed;

		[SerializeField] private Animator _animator;
		[SerializeField] private ColorPalette _colorPalette;
		[SerializeField] private Renderer[] _renderers;
		[SerializeField] private ParticleSystem[] _customColors;

		private readonly int _speedHash = Animator.StringToHash("Speed");
		private readonly int _attackHash = Animator.StringToHash("Attack");

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

			_player.OnAttack += OnAttack;
		}

		private void OnDestroy()
		{
			_player.OnAttack -= OnAttack;
		}


		private void Start()
		{
			int index = _player.PlayerId % _colorPalette.Colors.Length;
			Color color = _colorPalette.Colors[index];
			foreach (Renderer rend in _renderers)
			{
				rend.material.color = color;
			}

			foreach (ParticleSystem system in _customColors)
			{
				ParticleSystem.MainModule main = system.main;
				main.startColor = color;
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

		private void OnAttack()
		{
			_animator.SetTrigger(_attackHash);
		}

		private void SetAnimation()
		{
			float speed = _movable.DesiredVelocity.magnitude;
			_animator.SetFloat(_speedHash, speed);
		}
	}
}
