using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceShooter {
	[RequireComponent(typeof(Rigidbody))]
	public class Enemy : MonoBehaviour {

		private bool isTargetInSight;
		private Transform player;

		private Rigidbody _rigidbody;
		
		// Start is called before the first frame update
		void Start() {
			_rigidbody = GetComponent<Rigidbody>();
		}

		// Update is called once per frame
		void Update() {
			if (isTargetInSight) {
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), 500f);;
			} else {
				_rigidbody.AddRelativeForce(new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), Random.Range(5, 20)));
				_rigidbody.AddRelativeTorque(new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)));
			}
		}

		private void OnTriggerEnter(Collider other) {
			if (other.GetComponent<Player>()) {
				player = other.transform;
				isTargetInSight = true;
			}
		}

		private void OnTriggerExit(Collider other) {
			if (other.GetComponent<Player>()) {
				isTargetInSight = false;
			}
		}
	}
}
