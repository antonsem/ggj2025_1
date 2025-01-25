using UnityEngine;
using Random = UnityEngine.Random;

namespace BubbleHell
{
    public class BubbleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bubble;
        [SerializeField] private Transform ground;

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

        private void SpawnBubble()
        {
            Vector3 groundCenter = ground.position / 2;
            Vector3 groundCenterScale = ground.localScale / 2;

            float randX = Random.Range(groundCenter.x - groundCenterScale.x, groundCenter.x + groundCenterScale.x);
            float randZ = Random.Range(groundCenter.z - groundCenterScale.z, groundCenter.z + groundCenterScale.z);
            Vector3 randPos = new(randX, groundCenterScale.y * 2, randZ);

            Instantiate(bubble, randPos, Quaternion.identity, transform);
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
