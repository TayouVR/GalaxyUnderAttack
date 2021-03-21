using UnityEngine;

namespace SpaceShooter {
    public abstract class Destructible : MonoBehaviour {
        public Droptable droptable;
        public float health;
        public float shield;
        public float armor;

        public void TakeDamage(float kineticDamage, float electricDamage) {
            float shieldTemp = shield;
            float armorTemp = armor;
            float healthTemp = health;
            if (electricDamage - shield >= 0) {
                shieldTemp = 0;
                electricDamage -= shield;
            }
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

            shield = shieldTemp;
            armor = armorTemp;
            health = healthTemp;

            if (health == 0) {
                Destruct();
            }

        }

        protected virtual void Destruct() {
            Destroy(gameObject);
        }
    }
}
