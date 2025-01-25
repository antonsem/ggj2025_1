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
                // Commented out because Anton's system gives (intentional?) double calls upon the startbutton press.
                // GameOver > InGame calls.
                //ResetSpawning();
            }
        }

        private void StartSpawning()
        {
            if (_spawnCoroutine == null)
            {
                _spawnCoroutine = StartCoroutine(nameof(SpawnLoop));
            }
            else
            {
                Debug.LogWarning("Tried to start spawning coroutine whilst it's already on!");
            }
        }

        private IEnumerator SpawnLoop()
        {
            yield return new WaitUntil(_bubbleSpawner.BubblesArePurged);

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
            else
            {
                Debug.LogWarning("Tried to stop spawning coroutine whilst it's already off!");
            }
        }

        public void ResetSpawning(bool restartSpawning = false)
        {
            StopSpawning();

            StartCoroutine(_bubbleSpawner.PurgeBubbles());

            if (restartSpawning)
            {
                StartSpawning();
            }
        }
    }
}
