using UnityEngine;

namespace BubbleHell.Misc
{
	[CreateAssetMenu(fileName = "CameraConfig", menuName = "Sokoban/Camera Config", order = 0)]
	public class CameraConfig : ScriptableObject
	{
		[SerializeField] private float _maxShakeTime;
		[SerializeField] private float _maxShakeAmount;
		[SerializeField] private AnimationCurve _shakeCurve;

		public float MaxShakeTime => _maxShakeTime;

		public float MaxShakeAmount => _maxShakeAmount;

		public float GetShakeAmountFor(float shakeProgress)
		{
			return _shakeCurve.Evaluate(shakeProgress);
		}
	}
}
