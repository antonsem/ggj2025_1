using UnityEngine;

namespace BubbleHell.Interfaces
{
	public interface IBounceable
	{
		void Hit(IBounceable bounceable);
		void SetSpeed(float speed);
		void Bounce(bool bounce);
		void SetParent(Transform parent);
	}
}
