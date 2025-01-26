using System;

namespace BubbleHell.Misc
{
	public static class CameraShakeSignal
	{
		public static event Action<float, float> OnCameraShake;

		public static void ShakeCamera(float time, float amount)
		{
			OnCameraShake?.Invoke(time, amount);
		}
	}
}
