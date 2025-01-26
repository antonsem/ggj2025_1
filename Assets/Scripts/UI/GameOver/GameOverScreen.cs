using BubbleHell.Managers;
using BubbleHell.Misc;
using BubbleHell.Players;
using ExtraTools.UI.Screen;
using UnityEngine;
using UnityEngine.UI;

namespace BubbleHell.UI.GameOver
{
	public class GameOverScreen : ScreenBase
	{
		[SerializeField] private Image _playerImage;
		[SerializeField] private ColorPalette _colorPalette;
		[SerializeField] private PlayerManager _playerManager;

		private void Awake()
		{
			_playerManager.OnLastPlayer += OnLastPlayer;
		}

		private void OnDestroy()
		{
			_playerManager.OnLastPlayer -= OnLastPlayer;
		}

		private void OnLastPlayer(Player player)
		{
			_playerImage.color = _colorPalette.Colors[player.PlayerId];
		}
	}
}
