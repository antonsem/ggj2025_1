using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BubbleHell.Misc;
using BubbleHell.Players;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace BubbleHell.Managers
{
	public class PlayerManager : MonoBehaviour
	{
		public event Action<Player> OnLastPlayer;
		public event Action<Player> OnJoined;

		[SerializeField] private int _lifeCount = 2;
		[SerializeField] private PlayerInputManager _playerInputManager;
		[SerializeField] private GameStateManager _gameStateManager;
		[SerializeField] private Transform[] _spawnPositions;
		[SerializeField] private float _respawnDelay = 1;

		public int PlayerCount => _players.Count;

		private readonly List<Player> _players = new();

		private WaitForSeconds _respawnWait;

		#region Unity Methods

		private void Awake()
		{
			_respawnWait = new WaitForSeconds(_respawnDelay);
			_playerInputManager.onPlayerJoined += OnPlayerJoined;
			_playerInputManager.onPlayerLeft += OnPlayerLeft;
		}

		private void OnDestroy()
		{
			foreach (Player player in _players)
			{
				player.OnDied -= OnPlayerDeath;
				player.OnEliminated -= OnPlayerEliminated;
			}

			_playerInputManager.onPlayerJoined -= OnPlayerJoined;
			_playerInputManager.onPlayerLeft -= OnPlayerLeft;
		}

		#endregion

		public void Restart()
		{
			List<int> indices = new();

			for (int i = 0; i < _spawnPositions.Length; i++)
			{
				indices.Add(i);
			}

			indices.Shuffle();

			foreach (Player player in _players)
			{
				player.Heal(_lifeCount);
				player.Spawn(_spawnPositions[indices[0]]);
				indices.RemoveAt(0);
			}
		}

		public void ResetLives()
		{
			foreach (Player player in _players)
			{
				player.Heal(_lifeCount);
			}
		}

		private void OnPlayerJoined(PlayerInput player)
		{
			if(!player.transform.TryGetComponent(out Player p))
			{
				Debug.LogError($"Spawned player {player.name} does not have a {nameof(Player)} component!", this);
				return;
			}

			if(!_players.Contains(p))
			{
				p.Heal(_lifeCount);
				p.OnDied += OnPlayerDeath;
				p.OnEliminated += OnPlayerEliminated;
				_players.Add(p);
				OnJoined?.Invoke(p);
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

			if(!_players.Contains(p))
			{
				Debug.LogError($"{p.name} is not registered to {GetType().Name} named {name}. This shouldn't happen!", this);
				return;
			}

			p.OnDied -= OnPlayerDeath;
			p.OnEliminated -= OnPlayerEliminated;
			_players.Remove(p);
		}

		private void OnPlayerEliminated(Player _)
		{
			int remains = _players.Count(p => p.Lives >= 0);

			if(remains == 1)
			{
				OnLastPlayer?.Invoke(_players.Find(p => p.Lives >= 0));
			}
		}

		private void OnPlayerDeath(Player player)
		{
			if(_gameStateManager.CurrentGameState != GameState.InGame)
			{
				player.Heal(_lifeCount);
			}

			StartCoroutine(RespawnCoroutine(player));
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
