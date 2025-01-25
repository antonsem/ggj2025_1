using FMODUnity;
using UnityEngine;

public class FMODManager : MonoBehaviour
{
    private void Awake()
    {
		PlaySound("event:/MX_MainTheme");
    }

    public void PlaySound(string eventName)
	{
		RuntimeManager.PlayOneShot(eventName);
	}

	public void SetGlobalParameterValue(string parameterName, int parameterValue)
	{
		RuntimeManager.StudioSystem.setParameterByName(parameterName, parameterValue);
	}
}
