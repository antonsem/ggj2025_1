using FMODUnity;
using UnityEngine;

public class FMODManager : MonoBehaviour
{
	public void PlaySound(string eventName)
	{
		RuntimeManager.PlayOneShot(eventName);
	}

	public void SetGlobalParameterValue(string parameterName, int parameterValue)
	{
		RuntimeManager.StudioSystem.setParameterByName(parameterName, parameterValue);
	}
}
