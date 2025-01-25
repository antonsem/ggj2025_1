#define NOT_GIZMO
#if GIZMO
using System.Collections.Generic;
#endif
using UnityEngine;
using Random = UnityEngine.Random;

namespace BubbleHell
{
    public class BubbleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bubble;
        [SerializeField] private Transform ground;
        private float _sphereCastRadius;
        private int _layerMask;
        private const int _maxSpawnAttempts = 100;

#if GIZMO
        private readonly List<Vector3> _allowedPositions = new();
#endif

        private void Awake()
        {
            _sphereCastRadius = bubble.GetComponent<SphereCollider>().radius;
            _layerMask = ~(1 << LayerMask.NameToLayer("Ground"));
        }

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

        private bool TrySpawnBubble()
        {
            Vector3 groundCenter = ground.position / 2;
            Vector3 groundCenterScale = ground.localScale / 2;

            float groundLevel = ground.position.y + groundCenterScale.y + _sphereCastRadius;
            float randX = Random.Range(groundCenter.x - groundCenterScale.x, groundCenter.x + groundCenterScale.x);
            float randZ = Random.Range(groundCenter.z - groundCenterScale.z, groundCenter.z + groundCenterScale.z);

            Vector3 randPos = new(randX, groundLevel, randZ);

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
#if !GIZMO
                Instantiate(bubble, randPos, Quaternion.identity, transform);
#else
                _allowedPositions.Add(randPos);
#endif
                return true;
            }

            return false;
        }

        private void SpawnBubble()
        {
            int attempts = 0;

            while (attempts < _maxSpawnAttempts)
            {
                if (TrySpawnBubble())
                {
                    return;
                }

                attempts++;
            }
        }

        private void PurgeBubbles()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

#if GIZMO
            Debug.Log($"{_allowedPositions.Count} total allowed positions logged.");
            _allowedPositions.Clear();
#endif
        }

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
