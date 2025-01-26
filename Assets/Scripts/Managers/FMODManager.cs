using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class FMODManager : MonoBehaviour
{
    public void PlaySound(string eventName)
    {
        RuntimeManager.PlayOneShot(eventName);
    }

    public EventInstance CreateInstance(string audioPath)
    {
        return RuntimeManager.CreateInstance(audioPath);
    }

    public void StartMusic(EventInstance instance)
    {
        instance.start();
    }

    public void StopMusic(EventInstance instance)
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void SetGlobalParameterValue(string parameterName, int parameterValue)
    {
        RuntimeManager.StudioSystem.setParameterByName(parameterName, parameterValue);
    }
}

public static class AudioPath
{
    public const string MX_MainTheme = "event:/MX_MainTheme";
    public const string SFX_BubbleBounceWall = "event:/SFX_BubbleBounceWall";
    public const string SFX_BubblePoke = "event:/SFX_BubblePoke";
    public const string SFX_Damage = "event:/SFX_Damage";
    public const string SFX_Footstep = "event:/SFX_Footstep";
    public const string SFX_Death = "event:/SFX_Death";
    public const string SFX_Bumper = "event:/SFX_Bumper";
    public const string SFX_Pop = "event:/SFX_Pop";
}
