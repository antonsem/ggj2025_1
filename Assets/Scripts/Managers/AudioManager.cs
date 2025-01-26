using BubbleHell.Managers;
using BubbleHell.Players;
using UnityEngine;

namespace BubbleHell
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private FMODManager FMOD;

        void Awake()
        {
            PlayMainTheme();
        }

        private void OnEnable()
        {
            Hand.OnBubbleHit += PlayBubbleHit;
            BubblePhysics.Bubble.OnBounce += PlayBubbleBounce;
            Player.OnPlayerHit += PlayPlayerHit;
            //Player.OnPlayerMove += PlayPlayerMove;
        }

        private void OnDisable()
        {
            Hand.OnBubbleHit -= PlayBubbleHit;
            BubblePhysics.Bubble.OnBounce -= PlayBubbleBounce;
            Player.OnPlayerHit -= PlayPlayerHit;
            Player.OnPlayerMove -= PlayPlayerMove;
        }

        private void PlayMainTheme() => FMOD.PlaySound(AudioPath.MX_MainTheme);
        private void PlayBubbleHit() => FMOD.PlaySound(AudioPath.SFX_BubblePoke);
        private void PlayBubbleBounce() => FMOD.PlaySound(AudioPath.SFX_BubbleBounceWall);
        private void PlayPlayerHit() => FMOD.PlaySound(AudioPath.SFX_Damage);
        private void PlayPlayerMove() => FMOD.PlaySound(AudioPath.SFX_Footstep);
    }
}
