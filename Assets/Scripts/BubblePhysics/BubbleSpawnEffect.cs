using System.Collections.Generic;
using UnityEngine;

namespace BubbleHell.BubblePhysics
{
	public class BubbleSpawnEffect : MonoBehaviour
	{
		[SerializeField] private GameObject _spawnEffectPrefab;
		[SerializeField] private BubbleSpawner _bubbleSpawner;

		private readonly List<GameObject> _spawnedEffects = new();

		private void Awake()
		{
			_bubbleSpawner.OnStartSpawn += OnBubbleSpawn;
		}

		private void OnDestroy()
		{
			foreach (GameObject spawnedEffect in _spawnedEffects)
			{
				if(spawnedEffect)
				{
					spawnedEffect.GetComponent<ParticleSystem>().Stop();
					Destroy(spawnedEffect.gameObject);
				}
			}

			_spawnedEffects.Clear();
		}

		private void OnBubbleSpawn(Vector3 position)
		{
			foreach (GameObject spawnedEffect in _spawnedEffects)
			{
				if(!spawnedEffect.activeSelf)
				{
					spawnedEffect.transform.position = position;
					spawnedEffect.SetActive(true);
					spawnedEffect.GetComponent<ParticleSystem>().Stop();
					spawnedEffect.GetComponent<ParticleSystem>().Play();
					return;
				}
			}

			GameObject newEffect = Instantiate(_spawnEffectPrefab, position, Quaternion.identity);
			_spawnedEffects.Add(newEffect);
		}
	}
}
