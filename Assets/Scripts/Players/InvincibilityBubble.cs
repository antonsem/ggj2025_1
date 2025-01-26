using System.Collections;
using UnityEngine;

namespace BubbleHell.Players
{
	public class InvincibilityBubble : MonoBehaviour
	{
		[SerializeField] private GameObject _visual;
		[SerializeField] private AnimationCurve _popInCurve;
		[SerializeField] private float _speed;
		[SerializeField] private ParticleSystem _popBubbleEffect;

		private IEnumerator _popIn;
		private IEnumerator _popOut;

		public void Show()
		{
			if(!_visual.activeSelf)
			{
				Stop();
				_popIn = EnableCoroutine();
				StartCoroutine(_popIn);
			}
		}

		public void Hide()
		{
			if(_visual.activeSelf)
			{
				_visual.SetActive(false);
				_popBubbleEffect.Stop();
				_popBubbleEffect.Play();
			}
		}

		private IEnumerator EnableCoroutine()
		{
			_visual.SetActive(true);
			float t = 0;

			while (t < 1)
			{
				_visual.transform.localScale = Vector3.one * (_popInCurve.Evaluate(t) * 2);
				t += Time.deltaTime * _speed;
				yield return null;
			}
		}

		private void Stop()
		{
			if(_popOut != null)
			{
				StopCoroutine(_popOut);
				_popOut = null;
			}

			if(_popIn != null)
			{
				StopCoroutine(_popIn);
				_popIn = null;
			}
		}
	}
}
