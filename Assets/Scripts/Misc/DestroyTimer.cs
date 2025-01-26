using UnityEngine;

namespace BubbleHell.Misc
{
	public class DestroyTimer : MonoBehaviour
	{
		[SerializeField] private float _destroyAfter;

		private float _startTime;

		private void Start()
		{
			_startTime = Time.time;
		}

		private void Update()
		{
			if(Time.time - _startTime > _destroyAfter)
			{
				Destroy(gameObject);
			}
		}
	}
}
