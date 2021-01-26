using UnityEngine;

namespace SpaceShooter {
	[RequireComponent(typeof(Rigidbody))]
	public class Enemy : MonoBehaviour {

		private Rigidbody _rigidbody;
		// Start is called before the first frame update
		void Start() {
			_rigidbody = GetComponent<Rigidbody>();
		}

		// Update is called once per frame
		void Update() {
			_rigidbody.AddRelativeForce(new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2)));
			_rigidbody.AddRelativeTorque(new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)));
		}
	}
}
