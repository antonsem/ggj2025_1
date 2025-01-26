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
            Hand.OnHandUsed += PlayBirdPoke;
            BubblePhysics.Bubble.OnBounce += PlayBubbleBounce;
            Player.OnPlayerHit += PlayPlayerHit;
            Player.OnPlayerDeath += PlayPlayerDeath;
            Player.OnPlayerMove += PlayPlayerMove;
        }

        private void OnDisable()
        {
            Hand.OnHandUsed -= PlayBirdPoke;
            BubblePhysics.Bubble.OnBounce -= PlayBubbleBounce;
            Player.OnPlayerHit -= PlayPlayerHit;
            Player.OnPlayerMove -= PlayPlayerMove;
            Player.OnPlayerDeath -= PlayPlayerDeath;
        }

        private void PlayMainTheme() => FMOD.PlaySound(AudioPath.MX_MainTheme);
        private void PlayBirdPoke() => FMOD.PlaySound(AudioPath.SFX_BubblePoke);
        private void PlayBubbleBounce() => FMOD.PlaySound(AudioPath.SFX_BubbleBounceWall);
        private void PlayPlayerHit() => FMOD.PlaySound(AudioPath.SFX_Damage);
        private void PlayPlayerDeath() => FMOD.PlaySound(AudioPath.SFX_Death);
        private void PlayPlayerMove() => FMOD.PlaySound(AudioPath.SFX_Footstep);
    }
}
