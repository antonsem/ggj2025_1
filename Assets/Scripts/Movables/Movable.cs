using UnityEngine;

namespace BubbleHell.Movables
{
	public class Movable : MonoBehaviour
	{
		[SerializeField] private Rigidbody _rigid;
		[SerializeField] private MovableStats _stats;

		public Vector3 DesiredVelocity { get; private set; } = Vector3.zero;

		public Vector3 CurrentVelocity { get; private set; } = Vector3.zero;

		public MovableStats Stats
		{
			get => _stats;
			set
			{
				if(value)
				{
					_stats = value;
				}
			}
		}

		#region Unity Methods

		private void Awake()
		{
			if(!_rigid)
			{
				_rigid = GetComponent<Rigidbody>();
			}

			if(!Stats)
			{
				Stats = _stats;
			}
		}

		private void FixedUpdate()
		{
			CurrentVelocity = GetNewVelocity(_rigid.linearVelocity);
			_rigid.linearVelocity = CurrentVelocity;
		}

		#endregion

		public void Input(Vector3 input, bool forceDesiredVelocity = false)
		{
			DesiredVelocity = Vector3.ClampMagnitude(input, Stats.MaxSpeed);

			if(forceDesiredVelocity)
			{
				CurrentVelocity = DesiredVelocity;
				_rigid.linearVelocity = DesiredVelocity;
			}
		}

		public void Stop()
		{
			CurrentVelocity = Vector3.zero;
			DesiredVelocity = Vector3.zero;
			_rigid.linearVelocity = Vector3.zero;
		}

		private Vector3 GetNewVelocity(Vector3 currentVelocity)
		{
			if(Stats.IgnoreForce)
			{
				return DesiredVelocity;
			}

			Vector3 steering = Vector3.ClampMagnitude(DesiredVelocity - currentVelocity, Stats.MaxForce);
			currentVelocity = Vector3.ClampMagnitude(currentVelocity + steering, Stats.MaxSpeed);
			return currentVelocity;
		}
	}
}
