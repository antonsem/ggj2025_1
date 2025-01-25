using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BubbleHell.Players;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace BubbleHell.Managers
{
	public class PlayerManager : MonoBehaviour
	{
		public event Action<Player> OnLastPlayer;
		public event Action<Player> OnPayerEliminated;

		[SerializeField] private int _lifeCount = 3;
		[SerializeField] private PlayerInputManager _playerInputManager;
		[SerializeField] private Transform[] _spawnPositions;
		[SerializeField] private float _respawnDelay = 1;

		private readonly Dictionary<Player, int> _players = new();
		private CancellationTokenSource _cancellationTokenSource;

		private WaitForSeconds _respawnWait;

		#region Unity Methods

		private void Awake()
		{
			_respawnWait = new WaitForSeconds(_respawnDelay);
			_cancellationTokenSource = new CancellationTokenSource();
			_playerInputManager.onPlayerJoined += OnPlayerJoined;
			_playerInputManager.onPlayerLeft += OnPlayerLeft;
		}

		private void OnDestroy()
		{
			_playerInputManager.onPlayerJoined -= OnPlayerJoined;
			_playerInputManager.onPlayerLeft -= OnPlayerLeft;

			_cancellationTokenSource?.Cancel();
			_cancellationTokenSource?.Dispose();
			_cancellationTokenSource = null;
		}

		#endregion

		private void OnPlayerJoined(PlayerInput player)
		{
			if(!player.transform.TryGetComponent(out Player p))
			{
				Debug.LogError($"Spawned player {player.name} does not have a {nameof(Player)} component!", this);
				return;
			}

			if(_players.TryAdd(p, _lifeCount))
			{
				p.OnDied += OnPlayerDeath;
				return;
			}

			Debug.LogError($"{p.name} is already registered to {GetType().Name} named {name}. This shouldn't happen!", this);
		}

		private void OnPlayerLeft(PlayerInput player)
		{
			if(!player.transform.TryGetComponent(out Player p))
			{
				Debug.LogError($"Leaving player {player.name} does not have a {nameof(Player)} component!", this);
				return;
			}

			if(!_players.ContainsKey(p))
			{
				Debug.LogError($"{p.name} is not registered to {GetType().Name} named {name}. This shouldn't happen!", this);
				return;
			}

			_players.Remove(p);
		}

		private void OnPlayerDeath(Player player)
		{
			if(--_players[player] >= 0)
			{
				StartCoroutine(RespawnCoroutine(player));
			}
			else
			{
				OnPayerEliminated?.Invoke(player);

				int remains = _players.Values.Count(lives => lives >= 0);

				if(remains == 1)
				{
					OnLastPlayer?.Invoke(player);
				}
			}
		}

		private IEnumerator RespawnCoroutine(Player player)
		{
			yield return _respawnWait;

			int index = Random.Range(0, _spawnPositions.Length - 1);
			Transform spawnPos = _spawnPositions[index];

			player.Spawn(spawnPos);
		}
	}
}
