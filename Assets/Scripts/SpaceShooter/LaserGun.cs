using UnityEngine;

namespace SpaceShooter {
    public class LaserGun : Weapon {
        
        // Start is called before the first frame update
        void Start() {
        
        }

        // Update is called once per frame
        void Update() {
        
        }

        public override void Shoot() {
            GameObject flyingProjectile = Instantiate(projectile, transform.position, transform.rotation);
            flyingProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 20);
        }
    }
}
