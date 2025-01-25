using UnityEngine;

namespace BubbleHell.Interfaces
{
	public interface IBounceable
	{
		void Hit(IBounceable bounceable);
		void SetSpeed(float speed, Vector3 dir);
	}
}
