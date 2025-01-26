using System.Collections.Generic;
using BubbleHell.Managers;
using BubbleHell.Players;
using ExtraTools.UI.Panel;
using UnityEngine;

namespace BubbleHell.UI.InGame.Stats
{
	public class StatsPanel : PanelBase
	{
		[SerializeField] private PlayerManager _playerManager;
		[SerializeField] private Transform _content;
		[SerializeField] private GameObject _statPrefab;

		private readonly List<Stat> _stats = new();

		private void Awake()
		{
			_playerManager.OnJoined += OnPlayerJoined;
		}

		private void OnDestroy()
		{
			_playerManager.OnJoined -= OnPlayerJoined;
			foreach (Stat stat in _stats)
			{
				Destroy(stat.gameObject);
			}

			_stats.Clear();
		}

		private void OnPlayerJoined(Player player)
		{
			Stat stat = Instantiate(_statPrefab, _content).GetComponent<Stat>();
			stat.Setup(player);
			_stats.Add(stat);
		}
	}
}
