using System;
using BubbleHell.Interfaces;
using BubbleHell.Movables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BubbleHell.Players
{
	public class Player : MonoBehaviour, IBounceable
	{
		public event Action<Player> OnDied;

		[SerializeField] private InputActionAsset _inputAsset;
		[SerializeField] private Movable _movable;
		[SerializeField] private MonoBehaviour[] _disableOnDeathComponents;
		[SerializeField] private GameObject[] _disableOnDeathGameObjects;
		[SerializeField] private float _speed;
		[SerializeField] private Hand _hand;

		public Vector3 Velocity => _movable.CurrentVelocity;

		private Vector3 _input;
		private float _stunTimeLeft;
		private float _invincibilityTimeLeft;

		public void Move(InputAction.CallbackContext context)
		{
			Vector2 input = context.ReadValue<Vector2>();
			_input = new Vector3(input.x, 0, input.y);
		}

		public void Interact(InputAction.CallbackContext context)
		{
			if(_stunTimeLeft <= 0 && context.started)
			{
				_hand.UseHand();
			}
		}

		private void Update()
		{
			Vector3 velocity = _input.normalized * _speed;
			_movable.Input(velocity);
		}

		public void Spawn(Transform spawnPoint)
		{
			transform.position = spawnPoint.position;
			foreach (MonoBehaviour disableOnDeathComponent in _disableOnDeathComponents)
			{
				disableOnDeathComponent.enabled = true;
			}

			foreach (GameObject disableOnDeathGameObject in _disableOnDeathGameObjects)
			{
				disableOnDeathGameObject.SetActive(true);
			}
		}

		public void Hit(IBounceable bounceable)
		{
			_movable.Stop();
			foreach (MonoBehaviour disableOnDeathComponent in _disableOnDeathComponents)
			{
				disableOnDeathComponent.enabled = false;
			}

			foreach (GameObject disableOnDeathGameObject in _disableOnDeathGameObjects)
			{
				disableOnDeathGameObject.SetActive(false);
			}

			OnDied?.Invoke(this);
		}

		public void SetSpeed(float speed, Vector3 velocity = default)
		{
			_movable.Input(velocity * speed, true);
		}

#if UNITY_EDITOR

		[ContextMenu("Kill")]
		private void Kill()
		{
			Hit(this);
		}
#endif
	}
}
