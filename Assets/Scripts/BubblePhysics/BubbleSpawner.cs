#define NOT_GIZMO
#if GIZMO
using System.Collections.Generic;
#endif
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BubbleHell
{
    public class BubbleSpawner : MonoBehaviour
    {
        public static event Action OnPopBubble;
        public event Action<Vector3> OnStartSpawn;
        [SerializeField] private GameObject bubble;
        [SerializeField] private Transform ground;
        private float _sphereCastRadius;
        private int _layerMask;
        private const int _maxSpawnAttempts = 100;
        public int SpawnedBubblesCount { get; private set; }
        Vector3 groundCenter, groundCenterScale;
        private float _groundLevel;
        private bool _purgedBubbles = true;
        private readonly WaitForSeconds _spawnDelay = new(3);
        private readonly WaitForSeconds _popDelay = new(0.15f);
#if GIZMO
        private readonly List<Vector3> _allowedPositions = new();
#endif

        private void Awake()
        {
            _sphereCastRadius = bubble.GetComponent<SphereCollider>().radius;
            _layerMask = ~(1 << LayerMask.NameToLayer("Ground"));

            groundCenter = ground.position / 2;
            groundCenterScale = ground.localScale / 2;
            _groundLevel = ground.position.y + groundCenterScale.y + _sphereCastRadius;
        }

#if GIZMO
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SpawnBubble();
            }
            if (Input.GetKey(KeyCode.E))
            {
                SpawnBubble();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                PurgeBubbles();
            }
        }
#endif

        private IEnumerator DelayedSpawn(Vector3 randPos)
        {
            yield return _spawnDelay;
            Instantiate(bubble, randPos, Quaternion.identity, transform);
        }

        private bool TrySpawnBubble()
        {
            float randX = Random.Range(groundCenter.x - groundCenterScale.x, groundCenter.x + groundCenterScale.x);
            float randZ = Random.Range(groundCenter.z - groundCenterScale.z, groundCenter.z + groundCenterScale.z);
            Vector3 randPos = new(randX, _groundLevel, randZ);
#if GIZMO
            foreach (Vector3 position in _allowedPositions)
            {
                if (Vector3.Distance(position, randPos) < _sphereCastRadius * 2)
                {
                    return false;
                }
            }
#endif
            Collider[] hitColliders = Physics.OverlapSphere(randPos, _sphereCastRadius, _layerMask);
            if (hitColliders.Length == 0)
            {
                OnStartSpawn?.Invoke(randPos);
#if !GIZMO
                StartCoroutine(DelayedSpawn(randPos));
#else
                _allowedPositions.Add(randPos);
#endif
                return true;
            }

            return false;
        }

        public void SpawnBubble()
        {
            int attempts = 0;

            while (attempts < _maxSpawnAttempts)
            {
                if (TrySpawnBubble())
                {
                    SpawnedBubblesCount++;
                    return;
                }

                attempts++;
                if(attempts == _maxSpawnAttempts)
                {
                    Debug.LogWarning("Max spawn attempt limit hit!");
                }
            }
        }

        public IEnumerator PurgeBubbles()
        {
            _purgedBubbles = false;
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
                OnPopBubble?.Invoke();
                yield return _popDelay;
            }
            _purgedBubbles = true;
            SpawnedBubblesCount = 0;
#if GIZMO
            Debug.Log($"{_allowedPositions.Count} total allowed positions logged.");
            _allowedPositions.Clear();
#endif
        }
        public bool BubblesArePurged() => _purgedBubbles;
#if GIZMO
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            foreach (Vector3 position in _allowedPositions)
            {
                Gizmos.DrawWireSphere(position, _sphereCastRadius);
            }
        }
#endif
    }
}
