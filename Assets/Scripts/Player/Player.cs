using BubbleHell.Interfaces;
using BubbleHell.Movables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BubbleHell.Player
{
	public class Player : MonoBehaviour, IBounceable
	{
		[SerializeField] private InputActionAsset _inputAsset;
		[SerializeField] private Movable _movable;
		[SerializeField] private float _speed;
		[SerializeField] private Hand _hand;

		public Vector3 Velocity => _movable.CurrentVelocity;

		private InputAction _interactionAction;

		private Vector3 _input;
		private float _stunTimeLeft;
		private float _invincibilityTimeLeft;

		private void Awake()
		{
			_interactionAction = _inputAsset.FindAction("Interact");
		}

		private void OnEnable()
		{
			_interactionAction.Enable();
		}

		private void OnDisable()
		{
			_interactionAction.Disable();
		}

		public void Move(InputAction.CallbackContext context)
		{
			Vector2 input = context.ReadValue<Vector2>();
			_input = new Vector3(input.x, 0, input.y);
		}

		public void Interact(InputAction.CallbackContext context)
		{
			if(_stunTimeLeft <= 0 && context.started)
			{
				_hand.UseHand();
			}
		}

		private void FixedUpdate()
		{
			Vector3 velocity = _input.normalized * _speed;
			_movable.Input(velocity);
		}

		public void Hit(IBounceable bounceable)
		{
			// TODO: if not ball, bounce. Otherwise die
		}

		public void SetSpeed(float speed, Vector3 velocity = default)
		{
			//_movable.Input(velocity * speed, true);
		}
	}
}
