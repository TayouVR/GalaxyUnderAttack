using UnityEngine;

namespace SpaceShooter {
	public abstract class Weapon : MonoBehaviour {
        
		public new string name;
		public int damage;
		[SerializeField] protected GameObject projectile;

		public abstract void Shoot();

	}
}