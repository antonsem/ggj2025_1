using UnityEngine;

namespace BubbleHell.Players
{
    public class AnimationEvents : MonoBehaviour
    {
        [SerializeField] Player player;
        public void TriggerPlayerMoveEvent() => player.InvokeOnPlayerMove();
    }
}
