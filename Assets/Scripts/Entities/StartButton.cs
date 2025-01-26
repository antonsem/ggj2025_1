using System;
using BubbleHell.Interfaces;
using BubbleHell.Managers;
using BubbleHell.Players;
using UnityEngine;

namespace BubbleHell.Entities
{
	public class StartButton : MonoBehaviour, IBounceable
	{
		[SerializeField] private PlayerManager _playerManager;
		[SerializeField] private GameObject _pointCanvas;

		public event Action OnStart;
		public event Action OnNeedMorePlayers;

		public Vector3 Velocity => Vector3.zero;

		private void Awake()
		{
			_pointCanvas.SetActive(_playerManager.PlayerCount > 1);
		}

		private void OnEnable()
		{
			_playerManager.OnJoined += OnPlayerJoined;
		}

		private void OnDisable()
		{
			_playerManager.OnJoined += OnPlayerJoined;
		}

		private void OnPlayerJoined(Player _)
		{
			_pointCanvas.SetActive(_playerManager.PlayerCount > 1);
		}

		public void Hit(IBounceable bounceable)
		{
			if(bounceable is Player)
			{
				if(_playerManager.PlayerCount > 1)
				{
					OnStart?.Invoke();
					gameObject.SetActive(false);
				}
				else
				{
					OnNeedMorePlayers?.Invoke();
				}
			}
		}

		public void SetSpeed(float speed, Vector3 velocity = default)
		{
		}
	}
}
