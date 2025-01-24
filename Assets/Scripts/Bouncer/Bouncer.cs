using BubbleHell.Interfaces;
using UnityEngine;

namespace BubbleHell
{
    public class Bouncer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IBounceable bounceable))
            {
                bounceable.Bounce(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IBounceable bounceable))
            {
                bounceable.Bounce(false);
            }
        }
    }
}
