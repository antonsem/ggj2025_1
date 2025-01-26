using BubbleHell.Misc;
using BubbleHell.Players;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BubbleHell.UI.InGame.Stats
{
	public class Stat : MonoBehaviour
	{
		[SerializeField] private Image _playerImage;
		[SerializeField] private TMP_Text _lives;
		[SerializeField] private ColorPalette _colorPalette;

		private Player _player;

		public void Setup(Player player)
		{
			_playerImage.color = _colorPalette.Colors[player.PlayerId];
			player.OnLivesChanged += OnLivesChanged;
			OnLivesChanged(player.Lives);
			_player = player;
			gameObject.SetActive(true);
		}

		private void Start()
		{
			_playerImage.color = _colorPalette.Colors[_player.PlayerId];
		}

		private void OnLivesChanged(int lives)
		{
			if(lives < 0)
			{
				_lives.text = "X";
				_playerImage.color = _colorPalette.DisabledImage;
			}
			else
			{
				_playerImage.color = _player
					? _colorPalette.Colors[_player.PlayerId]
					: _colorPalette.DisabledImage;
				_lives.text = lives.ToString();
			}
		}

		private void OnDestroy()
		{
			_player.OnLivesChanged -= OnLivesChanged;
		}
	}
}
