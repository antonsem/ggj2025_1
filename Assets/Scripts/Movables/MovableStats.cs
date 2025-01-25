using UnityEngine;

namespace BubbleHell.Movables
{
	[CreateAssetMenu(fileName = "MovableStats", menuName = "Bubble Hell/Movable Stats", order = 0)]
	public class MovableStats : ScriptableObject
	{
		[field: SerializeField] public float MaxSpeed { get; private set; }

		[field: SerializeField] public float MaxForce { get; private set; }

		[field: SerializeField] public bool IgnoreForce { get; private set; }

		public void DebugSetMaxSpeed(float val)
		{
			MaxSpeed = val;
		}

		public void DebugSetMaxForce(float val)
		{
			MaxForce = val;
		}

		public void DebugSetIgnoreForce(bool val)
		{
			IgnoreForce = val;
		}
	}
}
