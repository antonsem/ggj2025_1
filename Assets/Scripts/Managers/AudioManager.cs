using BubbleHell.BubblePhysics;
using BubbleHell.Players;
using FMOD.Studio;
using System.Collections;
using UnityEngine;

namespace BubbleHell.Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private FMODManager FMOD;
        private EventInstance _mainTheme;

        private IEnumerator WaitUntilFMODInitalisation()
        {
            yield return new WaitUntil(FMOD.IsInitialised);
            _mainTheme = FMOD.CreateInstance(AudioPath.MX_MainTheme);
            StartMainTheme();
        }

        void Awake()
        {
           StartCoroutine(WaitUntilFMODInitalisation());
        }

        private void OnEnable()
        {
            Hand.OnHandUsed += PlayBirdPoke;
            Bubble.OnBounce += PlayBubbleBounce;
            Player.OnPlayerHit += PlayPlayerHit;
            Player.OnPlayerDeath += PlayPlayerDeath;
            Player.OnPlayerMove += PlayPlayerMove;
            BubbleSpawner.OnPopBubble += PlayBubblePop;
            Bouncer.OnBounce += PlayBumperHit;
        }

        private void OnDisable()
        {
            Hand.OnHandUsed -= PlayBirdPoke;
            Bubble.OnBounce -= PlayBubbleBounce;
            Player.OnPlayerHit -= PlayPlayerHit;
            Player.OnPlayerMove -= PlayPlayerMove;
            Player.OnPlayerDeath -= PlayPlayerDeath;
            BubbleSpawner.OnPopBubble -= PlayBubblePop;
            Bouncer.OnBounce -= PlayBumperHit;
        }

        public void StartMainTheme() => FMOD.StartMusic(_mainTheme);
        public void StopMainTheme() => FMOD.StopMusic(_mainTheme);

        private void PlayBirdPoke() => FMOD.PlaySound(AudioPath.SFX_BubblePoke);
        private void PlayBubbleBounce() => FMOD.PlaySound(AudioPath.SFX_BubbleBounceWall);
        private void PlayPlayerHit() => FMOD.PlaySound(AudioPath.SFX_Damage);
        private void PlayPlayerDeath() => FMOD.PlaySound(AudioPath.SFX_Death);
        private void PlayPlayerMove() => FMOD.PlaySound(AudioPath.SFX_Footstep);
        private void PlayBubblePop() => FMOD.PlaySound(AudioPath.SFX_Pop);
        private void PlayBumperHit() => FMOD.PlaySound(AudioPath.SFX_Bumper);
    }
}
