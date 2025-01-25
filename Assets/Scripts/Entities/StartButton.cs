using System;
using BubbleHell.Interfaces;
using BubbleHell.Players;
using UnityEngine;

namespace BubbleHell.Entities
{
	public class StartButton : MonoBehaviour, IBounceable
	{
		public event Action OnStart;

		public Vector3 Velocity => Vector3.zero;

		public void Hit(IBounceable bounceable)
		{
			if(bounceable is Player)
			{
				OnStart?.Invoke();
				gameObject.SetActive(false);
			}
		}

		public void SetSpeed(float speed, Vector3 velocity = default)
		{
		}
	}
}
