using System;
using UnityEngine;

namespace SpaceShooter {
	public class LaserProjectile : DamageCaster {
		public float lifetime = 10;
		void Update() {
			lifetime -= 1 * Time.deltaTime;
			if (lifetime <=  0) {
				Destroy(gameObject);
			}
		}
		private void OnCollisionEnter(Collision other) {
			other.collider.gameObject.GetComponent<Destructible>()?.TakeDamage(kineticDamage, electricDamage);
			Destroy(gameObject);
		}
	}
}