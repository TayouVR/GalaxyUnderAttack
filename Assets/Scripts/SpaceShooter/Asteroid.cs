using UnityEngine;

namespace SpaceShooter {
	public class Asteroid : Destructible {
		public GameObject dieParticleSystem;
		public string id;

		protected override void Destruct() {
			Instantiate(dieParticleSystem, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}