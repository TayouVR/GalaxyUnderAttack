using UnityEngine;

namespace SpaceShooter {
	[RequireComponent(typeof(Rigidbody))]
	public class Player : MonoBehaviour {
	
		private InputActions _inputActions;

		private Vector3 rotation;
		private Vector3 movement;

		private Rigidbody _rigidbody;

		private void Awake() {
		
			_rigidbody = GetComponent<Rigidbody>();
			_inputActions = new InputActions();
		
			//movement
			_inputActions.ShipControls.MoveLeftRight.performed += context => movement.x = context.ReadValue<float>();
			_inputActions.ShipControls.MoveUpDown.performed += context => movement.y = context.ReadValue<float>();
			_inputActions.ShipControls.MoveForwardBack.performed += context => movement.z = -context.ReadValue<float>();
		
			//rotation
			_inputActions.ShipControls.RotateUpDown.performed += context => rotation.x = -context.ReadValue<float>();
			_inputActions.ShipControls.RotateLeftRight.performed += context => rotation.y = context.ReadValue<float>();
			_inputActions.ShipControls.Roll.performed += context => rotation.z = -context.ReadValue<float>()*5;
		}

		// Update is called once per frame
		void FixedUpdate() {
			_rigidbody.AddRelativeForce(movement*50);
			_rigidbody.AddRelativeTorque(rotation*1.3f);
		}

		private void OnEnable() {
			_inputActions.Enable();
		}

		private void OnDisable() {
			_inputActions.Disable();
		}
	}
}
