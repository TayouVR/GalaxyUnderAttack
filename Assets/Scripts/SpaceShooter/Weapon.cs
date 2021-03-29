using UnityEngine;

namespace SpaceShooter {
	public abstract class Weapon : MonoBehaviour {
        
		public new string name;
		public int damage;
		[SerializeField] protected GameObject projectile;

		public GameObject soundEffect;

		public virtual void Shoot() {
			if (soundEffect != null) {
				var sfx = Instantiate(soundEffect, transform.position, transform.rotation);
				var audioSource = sfx.GetComponent<AudioSource>();
				audioSource.volume = GameManager.Instance.miscVolume;
				audioSource.Play();
				sfx.GetComponent<DelayedDestroyer>().StartCountdown();
			}
		}

	}
}