using System.Collections;
using UnityEngine;

namespace BubbleHell
{
    public class BubbleSpawnerManager : MonoBehaviour
    {
        [SerializeField] private int minSeconds, maxSeconds; 
        private Coroutine _spawnCoroutine;

        private BubbleSpawner _bubbleSpawner;

        private const int bubbleSpawnLimit = 100;

        private void Awake()
        {
            _bubbleSpawner = GetComponent<BubbleSpawner>();
        }

        private void Start()
        {
            StartSpawning();
        }

        private void StartSpawning()
        {
            _spawnCoroutine = StartCoroutine(nameof(SpawnLoop));
        }

        private IEnumerator SpawnLoop() 
        { 
            while(true)
            {
                _bubbleSpawner.SpawnBubble();
                if(_bubbleSpawner.SpawnedBubblesCount == bubbleSpawnLimit)
                {
                    Debug.LogWarning("Bubble spawn limit has been reached! Exited spawning coroutine.");
                    break;
                }

                int randWaitForSeconds = Random.Range(minSeconds, maxSeconds);
                yield return new WaitForSeconds(randWaitForSeconds);
            }
        }

        public void StopSpawning()
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }

        public void ResetSpawning(bool restartSpawning = false)
        {
            StopSpawning();

            _bubbleSpawner.PurgeBubbles();

            if(restartSpawning)
            {
                StartSpawning();
            }
        }
    }
}
