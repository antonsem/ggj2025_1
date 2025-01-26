using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BubbleHell.Misc
{
	[RequireComponent(typeof(Camera))]
	public class CameraShake : MonoBehaviour
	{
		[SerializeField] private CameraConfig _config;

		private Camera _cam;

		private IEnumerator _shake;
		private Vector3 _camPos;
		private float _shakeTime;
		private float _shakeAmount;

		private void Awake()
		{
			_cam = GetComponent<Camera>();
			CameraShakeSignal.OnCameraShake += Shake;
		}

		private void OnDestroy()
		{
			CameraShakeSignal.OnCameraShake -= Shake;
		}

		private void Shake(float shakeTime, float shakeAmount)
		{
			if(_shake != null)
			{
				_shakeTime = Mathf.Clamp(_shakeTime + shakeTime * 0.5f, 0, _config.MaxShakeTime);
				_shakeAmount = Mathf.Clamp(_shakeAmount + shakeAmount * 0.5f, 0, _config.MaxShakeAmount);
				return;
			}

			_shakeTime = Mathf.Clamp(shakeTime, 0, _config.MaxShakeTime);
			_shakeAmount = Mathf.Clamp(shakeAmount, 0, _config.MaxShakeAmount);

			_shake = ShakeCoroutine();
			StartCoroutine(_shake);
		}

		private IEnumerator ShakeCoroutine()
		{
			_camPos = _cam.transform.localPosition;
			float t = 0f;

			while (t < _shakeTime)
			{
				float multiplier = _shakeAmount * _config.GetShakeAmountFor(t / _shakeTime);
				_cam.transform.localPosition = _camPos + Random.insideUnitSphere * multiplier;

				t += Time.unscaledDeltaTime;
				yield return null;
			}

			_cam.transform.localPosition = _camPos;
			_shake = null;
		}
	}
}
