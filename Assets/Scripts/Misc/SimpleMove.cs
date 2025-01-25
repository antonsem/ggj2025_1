using UnityEngine;

namespace BubbleHell
{
    public class SimpleMove : MonoBehaviour
    {
        [SerializeField] int _moveSpeed;

        void Update()
        {
            if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.forward / _moveSpeed);
            if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left / _moveSpeed);
            if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.back / _moveSpeed);
            if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right / _moveSpeed);
        }
    }
}
