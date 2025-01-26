using UnityEngine;

namespace BubbleHell.Misc
{
	public class PointDown : MonoBehaviour
	{
		[SerializeField] private Vector3 _offset;
		[SerializeField] private float _speed;

		private Vector3 _start;
		private Vector3 _end;

		private Vector3 _nextPos;
		private Vector3 _prevPos;

		private float _progress;

		private void Awake()
		{
			_start = transform.position;
			_end = _start + _offset;
			_prevPos = _start;
			_nextPos = _end;
		}

		private void Update()
		{
			transform.LookAt(Camera.main.transform.position);

			transform.position = Vector3.Lerp(_prevPos, _nextPos, _progress);
			_progress += Time.deltaTime * _speed;

			if(_progress >= 1)
			{
				_progress = 0;
				(_nextPos, _prevPos) = (_prevPos, _nextPos);
			}
		}
	}
}
