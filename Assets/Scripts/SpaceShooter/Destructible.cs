using UnityEngine;

namespace SpaceShooter {
    public abstract class Destructible : MonoBehaviour {
        public Droptable droptable;
        public int health;
        public int shield;
        public int armor;

        public void GetDamage(int kineticDamage, int electricDamage) {
            int shieldTemp = shield;
            int armorTemp = armor;
            int healthTemp = health;
            if (electricDamage - shield >= 0) {
                shieldTemp = 0;
                electricDamage -= shield;
            } else {
                shieldTemp = shield - electricDamage;
                if (shieldTemp - kineticDamage/2 <= 0) {
                    armorTemp += shieldTemp*2;
                    shieldTemp = 0;
                    if (armorTemp <= 0) {
                        healthTemp += armorTemp;
                        armorTemp = 0;
                        if (healthTemp <= 0) {
                            healthTemp = 0;
                        }
                    }
                }
            }

            shield = shieldTemp;
            armor = armorTemp;
            health = healthTemp;

            if (armor == 0) {
                Destruct();
            }

        }

        protected void Destruct() {
            Destroy(gameObject);
        }
    }
}
