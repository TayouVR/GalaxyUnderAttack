using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

namespace SpaceShooter {
    public abstract class Destructible : MonoBehaviour {
        public Droptable droptable;
        public float health;
        public float shield;
        public float armor;
        public int scoreValue;

        public GameObject soundEffect;

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
                string value = ((TextElement) GameManager.Instance.scoreLabel).text;
                ((TextElement) GameManager.Instance.scoreLabel).text = (int.Parse(value) + scoreValue).ToString(); 
                if (soundEffect != null) {
                    var sfx = Instantiate(soundEffect, transform.position, transform.rotation);
                    var audioSource = sfx.GetComponent<AudioSource>();
                    audioSource.volume = GameManager.Instance.miscVolume;
                    audioSource.Play();
                    sfx.GetComponent<DelayedDestroyer>().StartCountdown();
                }
                Destruct();
            }

        }

        protected virtual void Destruct() {
            Destroy(gameObject);
        }
    }
}
