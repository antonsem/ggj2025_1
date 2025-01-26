using System;
using BubbleHell.Entities;
using BubbleHell.Players;
using BubbleHell.UI.GameOver;
using TheBubbleHell.UI;
using TheBubbleHell.UI.InGame;
using TheBubbleHell.UI.Lobby;
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
		[SerializeField] private UIManager _uiManager;

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

		private LobbyScreen _lobbyScreen;
		private InGameScreen _inGameScreen;
		private GameOverScreen _gameOverScreen;

		private float _gameOverTime = 0;

		#region Unity Methods

		private void Awake()
		{
			_lobbyScreen = _uiManager.GetScreen<LobbyScreen>();
			_inGameScreen = _uiManager.GetScreen<InGameScreen>();
			_gameOverScreen = _uiManager.GetScreen<GameOverScreen>();

			InputActionMap map = _inputActionAsset.FindActionMap("GameStateMap");
			_restartAction = map.FindAction("Restart");
			_startButton = FindFirstObjectByType<StartButton>();
			if(!_startButton)
			{
				_currentGameState = GameState.GameOver;
				OnRestartAction(default);
			}
		}

		private void Start()
		{
			if(CurrentGameState != GameState.InLobby)
			{
				CurrentGameState = GameState.InLobby;
			}
			else
			{
				OnGameStateChanged?.Invoke(CurrentGameState);
			}

			_lobbyScreen.Show();
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
			_playerManager.OnJoined += OnPlayerJoined;
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

		private void OnPlayerJoined(Player _)
		{
			if(CurrentGameState == GameState.InLobby && _playerManager.PlayerCount == 1)
			{
				_lobbyScreen.ShowLobby();
			}
		}

		private void OnRestart()
		{
			if(CurrentGameState == GameState.InLobby)
			{
				_playerManager.Restart();
				CurrentGameState = GameState.InGame;
				_inGameScreen.Show();
			}
		}

		private void OnGameOver(Player _)
		{
			_gameOverTime = Time.time;
			CurrentGameState = GameState.GameOver;
			_gameOverScreen.Show();
		}

		private void OnRestartAction(InputAction.CallbackContext _)
		{
			if(CurrentGameState == GameState.GameOver && Time.time - _gameOverTime >= 1)
			{
				_playerManager.Restart();
				CurrentGameState = GameState.InGame;
				_inGameScreen.Show();
			}
		}
	}
}
