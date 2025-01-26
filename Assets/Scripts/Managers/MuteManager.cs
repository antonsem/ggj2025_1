using UnityEngine;
using UnityEngine.InputSystem;

namespace BubbleHell.Managers
{
	public class MuteManager : MonoBehaviour
	{
		[SerializeField] private AudioManager _audioManager;
		[SerializeField] private InputActionAsset _inputActionAsset;

		private InputAction _muteAction;
		private bool _muted;

		private void Awake()
		{
			InputActionMap map = _inputActionAsset.FindActionMap("SystemMap");
			_muteAction = map.FindAction("Mute");
		}

		private void OnEnable()
		{
			_muteAction.performed += OnMute;
		}

		private void OnDisable()
		{
			_muteAction.performed -= OnMute;
		}

		private void OnMute(InputAction.CallbackContext _)
		{
			if(_muted)
			{
				_audioManager.StartMainTheme();
				_muted = false;
			}
			else
			{
				_audioManager.StopMainTheme();
				_muted = true;
			}
		}
	}
}
