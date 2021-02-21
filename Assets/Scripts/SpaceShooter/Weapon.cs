using UnityEngine;

namespace SpaceShooter {
    public class Weapon : MonoBehaviour {
        
        public new string name;
        public int damage;
        [SerializeField] private GameObject projectile;
        
        // Start is called before the first frame update
        void Start() {
        
        }

        // Update is called once per frame
        void Update() {
        
        }

        public void Shoot() {
            GameObject flyingProjectile = Instantiate(projectile, transform.position, transform.rotation);
            flyingProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 20);
        }
    }
}
