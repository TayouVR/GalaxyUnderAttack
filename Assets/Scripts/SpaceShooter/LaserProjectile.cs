using System;
using UnityEngine;

namespace SpaceShooter {
	public class LaserProjectile : DamageCaster {
		
		public float lifetime = 10;
		public GameObject soundEffect;
		
		void Update() {
			lifetime -= 1 * Time.deltaTime;
			if (lifetime <=  0) {
				Destroy(gameObject);
			}
		}
		private void OnCollisionEnter(Collision other) {
			other.collider.gameObject.GetComponent<Destructible>()?.TakeDamage(kineticDamage, electricDamage);
			
			if (soundEffect != null) {
				var sfx = Instantiate(soundEffect, transform.position, transform.rotation);
				var audioSource = sfx.GetComponent<AudioSource>();
				audioSource.volume = GameManager.Instance.miscVolume;
				audioSource.Play();
				sfx.GetComponent<DelayedDestroyer>().StartCountdown();
			}
			Destroy(gameObject);
		}
	}
}