﻿using System;
using BubbleHell.Interfaces;
using BubbleHell.Movables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BubbleHell.Players
{
	public class Player : MonoBehaviour, IBounceable
	{
		public event Action<int> OnLivesChanged;
		public event Action<Player> OnDied;
		public event Action<Player> OnEliminated;

		[SerializeField] private PlayerInput _playerInput;
		[SerializeField] private InputActionAsset _inputAsset;
		[SerializeField] private Movable _movable;
		[SerializeField] private MonoBehaviour[] _disableOnDeathComponents;
		[SerializeField] private GameObject[] _disableOnDeathGameObjects;
		[SerializeField] private float _speed;
		[SerializeField] private Hand _hand;

		public int PlayerId { get; private set; }
		private int _lives;

		public int Lives
		{
			get => _lives;
			private set
			{
				if(_lives != value)
				{
					_lives = value;
					OnLivesChanged?.Invoke(value);
				}
			}
		}

		public Vector3 Velocity => _movable.CurrentVelocity;

		private Vector3 _input;
		private float _invincibilityTimeLeft;

		#region Unity Methods

		private void Awake()
		{
			PlayerId = _playerInput.playerIndex;
		}

		private void Update()
		{
			Vector3 velocity = _input.normalized * _speed;
			_movable.Input(velocity);
		}

		#endregion

		public void Heal(int lives)
		{
			Lives = lives;
		}

		public void OnInteract(InputAction.CallbackContext context)
		{
			if(enabled && context.started)
			{
				_hand.UseHand();
			}
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			Vector2 input = context.ReadValue<Vector2>();
			_input = new Vector3(input.x, 0, input.y);
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
			if(bounceable is not Player && bounceable.Velocity.sqrMagnitude < 20)
			{
				return;
			}

			_movable.Stop();
			foreach (MonoBehaviour disableOnDeathComponent in _disableOnDeathComponents)
			{
				disableOnDeathComponent.enabled = false;
			}

			foreach (GameObject disableOnDeathGameObject in _disableOnDeathGameObjects)
			{
				disableOnDeathGameObject.SetActive(false);
			}

			if(--Lives >= 0)
			{
				OnDied?.Invoke(this);
			}
			else
			{
				OnEliminated?.Invoke(this);
			}
		}

		public void SetSpeed(float speed, Vector3 velocity = default)
		{
			_movable.Input(velocity * speed, true);
		}

		[ContextMenu("Kill")]
		public void Kill()
		{
			Hit(this);
		}
	}
}
