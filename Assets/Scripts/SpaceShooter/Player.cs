using UnityEngine;

namespace SpaceShooter {
	[RequireComponent(typeof(Rigidbody))]
	public class Player : MonoBehaviour {
	
		private InputActions _inputActions;

		private Vector3 _rotation;
		private Vector3 _movement;

		private Rigidbody _rigidbody;

		private bool enableMovement;

		private void Awake() {
		
			_rigidbody = GetComponent<Rigidbody>();
			_inputActions = new InputActions();
		
			//movement
			_inputActions.ShipControls.MoveLeftRight.performed += context => _movement.x = context.ReadValue<float>();
			_inputActions.ShipControls.MoveUpDown.performed += context => _movement.y = context.ReadValue<float>();
			_inputActions.ShipControls.MoveForwardBack.performed += context => _movement.z = -context.ReadValue<float>();
		
			//rotation
			_inputActions.ShipControls.RotateUpDown.performed += context => _rotation.x = -context.ReadValue<float>();
			_inputActions.ShipControls.RotateLeftRight.performed += context => _rotation.y = context.ReadValue<float>();
			_inputActions.ShipControls.Roll.performed += context => _rotation.z = -context.ReadValue<float>()*5;
		}

		private void Init() {
			enableMovement = true;
		}

		private void Pause() {
			enableMovement = false;
		}

		// Update is called once per frame
		private void FixedUpdate() {
			if (enableMovement) {
				_rigidbody.AddRelativeForce(_movement*100);
				_rigidbody.AddRelativeTorque(_rotation*1.3f);
			}
		}

		private void OnEnable() {
			_inputActions.Enable();
		}

		private void OnDisable() {
			_inputActions.Disable();
		}
	}
}
