using UnityEngine;

namespace SpaceShooter {
    public class LaserGun : Weapon {

        public override void Shoot() {
            GameObject flyingProjectile = Instantiate(projectile, transform.position, transform.rotation);
            flyingProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 20);
			
            base.Shoot();
        }
    }
}
