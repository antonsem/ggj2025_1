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
            if (Input.GetKey(KeyCode.R))
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

            Collider[] hitColliders = Physics.OverlapSphere(randPos, _sphereCastRadius, _layerMask);
            if (hitColliders.Length == 0)
            {
                Instantiate(bubble, randPos, Quaternion.identity, transform);
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
        }
    }
}
