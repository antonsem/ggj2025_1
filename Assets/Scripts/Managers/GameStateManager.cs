using System;
using BubbleHell.Entities;
using BubbleHell.Players;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BubbleHell.Managers
{
	public enum GameState
	{
		InLobby,
		InGame,
		GameOver
	}

	public class GameStateManager : MonoBehaviour
	{
		public event Action<GameState> OnGameStateChanged;

		[SerializeField] private InputActionAsset _inputActionAsset;
		[SerializeField] private PlayerInputManager _playerInputManager;
		[SerializeField] private PlayerManager _playerManager;

		private GameState _currentGameState = GameState.InLobby;

		public GameState CurrentGameState
		{
			get => _currentGameState;
			private set
			{
				if(_currentGameState == value)
				{
					return;
				}

				_currentGameState = value;
				OnGameStateChanged?.Invoke(value);
			}
		}

		private InputAction _restartAction;
		private StartButton _startButton;

		#region Unity Methods

		private void Awake()
		{
			InputActionMap map = _inputActionAsset.FindActionMap("GameStateMap");
			_restartAction = map.FindAction("Restart");
			_startButton = FindFirstObjectByType<StartButton>();
			if(!_startButton)
			{
				OnRestartAction(default);
			}
		}

		private void OnEnable()
		{
			if(_startButton)
			{
				_startButton.OnStart += OnRestart;
			}
			else
			{
				CurrentGameState = GameState.InGame;
			}

			if(CurrentGameState == GameState.InGame)
			{
				_restartAction.Disable();
			}
			else
			{
				_restartAction.Enable();
			}

			_restartAction.performed += OnRestartAction;
			_playerManager.OnLastPlayer += OnGameOver;
		}

		private void OnDisable()
		{
			if(_startButton)
			{
				_startButton.OnStart -= OnRestart;
			}

			_restartAction.Disable();
			_restartAction.performed -= OnRestartAction;
			_playerManager.OnLastPlayer -= OnGameOver;
		}

		#endregion

		private void OnRestart()
		{
			CurrentGameState = GameState.GameOver;
			OnRestartAction(default);
		}

		private void OnGameOver(Player _)
		{
			CurrentGameState = GameState.GameOver;
		}

		private void OnRestartAction(InputAction.CallbackContext _)
		{
			if(CurrentGameState == GameState.GameOver)
			{
				_playerManager.Restart();
				CurrentGameState = GameState.InGame;
			}
		}
	}
}
