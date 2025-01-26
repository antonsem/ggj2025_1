using System;
using UnityEngine;

namespace BubbleHell.Players
{
	public class AttackTrigger : MonoBehaviour
	{
		public event Action OnAttacked;

		public void DoAttack()
		{
			OnAttacked?.Invoke();
		}
	}
}
