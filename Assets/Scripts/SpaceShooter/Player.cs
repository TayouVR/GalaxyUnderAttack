using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceShooter {
	[RequireComponent(typeof(Rigidbody))]
	public class Player : MonoBehaviour, IShipOwner {
	
		private InputActions _inputActions;

		private Vector3 _rotation;
		private Vector3 _movement;

		private Rigidbody _rigidbody;

		private bool enableMovement;

		private GameObject spawnedShip;
		private Ship spawnedShipShip;

		private void Awake() {
		
			_rigidbody = GetComponent<Rigidbody>();
			_inputActions = new InputActions();
		
			//movement
			_inputActions.ShipControls.MoveLeftRight.performed += context => _movement.x = context.ReadValue<float>()/4;
			_inputActions.ShipControls.MoveUpDown.performed += context => _movement.y = context.ReadValue<float>()/4;
			_inputActions.ShipControls.MoveForwardBack.performed += context => _movement.z = -context.ReadValue<float>();
		
			//rotation
			_inputActions.ShipControls.RotateUpDown.performed += context => _rotation.x = -context.ReadValue<float>();
			_inputActions.ShipControls.RotateLeftRight.performed += context => _rotation.y = context.ReadValue<float>();
			_inputActions.ShipControls.Roll.performed += context => _rotation.z = -context.ReadValue<float>()*5;
		}

		public void SetWeapon(int index, GameObject prefab) {
			if ((object)spawnedShipShip != null) {
				if (spawnedShipShip.weaponComponents.Length == 0) {
					spawnedShipShip.weaponComponents = new Weapon[spawnedShipShip.weapons.Length];
				}
				spawnedShipShip.weaponComponents[index] = Instantiate(prefab, spawnedShipShip.weapons[index]).GetComponent<Weapon>();
			}
		}

		public void PrimaryShoot(InputAction.CallbackContext context) {
			if ((object)spawnedShipShip != null) {
				spawnedShipShip.Shoot();
			}
		}
		
		IEnumerator Fire() {
			while (_inputActions.ShipControls.PrimaryFire.ReadValue<float>() >= 0.5f) {
				if ((object)spawnedShipShip != null) {
					spawnedShipShip.Shoot();
				}
				yield return new WaitForSeconds(0.5f);
			}
		}

		public void SecondaryShoot(InputAction.CallbackContext context) {
			if ((object)spawnedShipShip != null) {
				spawnedShipShip.Shoot();
			}
		}

		public void Init() {
			enableMovement = true;

			_inputActions.ShipControls.PrimaryFire.performed += PrimaryShoot;
			_inputActions.ShipControls.SecondaryFire.performed += SecondaryShoot;
			//StartCoroutine(Fire());
		}

		public void Pause() {
			enableMovement = false;

			_inputActions.ShipControls.PrimaryFire.performed -= PrimaryShoot;
			_inputActions.ShipControls.SecondaryFire.performed -= SecondaryShoot;
		}

		public void SetShip(GameObject shipPrefab) {
			if ((object)spawnedShip != null) {
				Destroy(spawnedShip);
			}
			spawnedShip = Instantiate(shipPrefab, transform);
			spawnedShipShip = spawnedShip.GetComponent<Ship>();
			spawnedShipShip.fraction = Ship.Fraction.PLAYER;
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

		public void Die() {
			Destroy(gameObject);
		}
	}
}
