using BubbleHell.Managers;
using System.Collections;
using UnityEngine;

namespace BubbleHell
{
    public class BubbleSpawnerManager : MonoBehaviour
    {
        [SerializeField] private int minSeconds, maxSeconds;
        [SerializeField] private GameStateManager gameStateManager;
        private Coroutine _spawnCoroutine;

        private BubbleSpawner _bubbleSpawner;

        private const int bubbleSpawnLimit = 8;

        private void Awake()
        {
            _bubbleSpawner = transform.GetChild(0).GetComponent<BubbleSpawner>();
        }

        private void Start()
        {
            StartSpawning();
        }

        private void OnEnable()
        {
            gameStateManager.OnGameStateChanged += HandleGameStateChange;
        }

        private void OnDisable()
        {
            gameStateManager.OnGameStateChanged -= HandleGameStateChange;
        }

        private void HandleGameStateChange(GameState gameState)
        {
            if (gameState != GameState.GameOver)
            {
                ResetSpawning(true);
            }
            else
            {
                ResetSpawning();
            }
        }

        private void StartSpawning()
        {
            _spawnCoroutine = StartCoroutine(nameof(SpawnLoop));
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                _bubbleSpawner.SpawnBubble();
                if (_bubbleSpawner.SpawnedBubblesCount == bubbleSpawnLimit)
                {
                    // Commented this out because this is intentional now.
                    //Debug.LogWarning("Bubble spawn limit has been reached! Exited spawning coroutine.");
                    break;
                }

                int randWaitForSeconds = Random.Range(minSeconds, maxSeconds);
                yield return new WaitForSeconds(randWaitForSeconds);
            }
        }

        public void StopSpawning()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }

        public void ResetSpawning(bool restartSpawning = false)
        {
            StopSpawning();

            _bubbleSpawner.PurgeBubbles();

            if (restartSpawning)
            {
                StartSpawning();
            }
        }
    }
}
