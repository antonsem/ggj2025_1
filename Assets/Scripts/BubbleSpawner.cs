using UnityEngine;
using Random = UnityEngine.Random;

namespace BubbleHell
{
    public class BubbleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bubble;
        [SerializeField] private Transform corner1, corner2;
        [SerializeField] private Transform bubbleParent;
        [SerializeField] private Transform pickupField;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SpawnBubble(pickupField);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                SpawnBubble();
            }
        }

        private void SpawnBubble(Transform spawnPos = null)
        {
            float randX = Random.Range(corner1.position.x, corner2.position.x);
            float randZ = Random.Range(corner1.position.z, corner2.position.z);
            Vector3 randPos = new(randX, 1, randZ);

            Instantiate(bubble, spawnPos == null ? randPos : spawnPos.position, Quaternion.identity, bubbleParent);
        }
    }
}
