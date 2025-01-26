using System.Collections;
using UnityEngine;

namespace BubbleHell.BubblePhysics
{
	public class BubbleInEffect : MonoBehaviour
	{
		[SerializeField] private Transform _visuals;
		[SerializeField] private AnimationCurve _popInCurve;
		[SerializeField] private float _speed;

		private void Awake()
		{
			_visuals.localScale = Vector3.zero;
		}

		private IEnumerator Start()
		{
			float t = 0;

			while (t < 1)
			{
				_visuals.localScale = Vector3.one * _popInCurve.Evaluate(t);
				t += Time.deltaTime * _speed;
				yield return null;
			}
		}
	}
}
