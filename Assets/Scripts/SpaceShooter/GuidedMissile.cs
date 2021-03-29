using System;
using System.Collections;
using System.Collections.Generic;
using SpaceShooter;
using UnityEngine;

public class GuidedMissile : DamageCaster {
    // Start is called before the first frame update

    public GameObject target;
    public float speed = 0.5f;
    public float turnSpeed = 10;
    public float lifetime = 10;
    public GameObject explosionEffectPrefab;
    public bool explodeOnLifeExpire = false;
    public float tolorateDist = 10;
    private float distance = 10000;
    public float explosionRadius = 25;

    public bool missileReady = false;

    public GameObject soundEffect;

    void Start() {

    }
    void Update() {
        lifetime -= 1 * Time.deltaTime;
    }
    
    void LateUpdate() {

        if (missileReady) {
            if (target != null) {
                distance = Vector3.Distance(transform.position, target.transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), turnSpeed * Time.deltaTime);
            }
        
            transform.position += transform.forward * speed;
        
            if (lifetime <= 0 && explodeOnLifeExpire || distance < tolorateDist) {
                explode();
            }
            if (lifetime <=  0) {
                killBullet();
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        explode();
    }

    public void killBullet() {
        Destroy(this.gameObject);
        Destroy(this);
    }
    
    public void explode() {

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hits) {
            GameObject hitObj = hit.gameObject;
            float DistanceDamageMultiplier = Vector3.Distance(transform.position, hitObj.transform.position) / 10;
            Destructible dst = hitObj.GetComponent<Destructible>();
            if (dst != null) {
                //Debug.Log("DAMAGE! " + kineticDamage * DistanceDamageMultiplier + " " + electricDamage * DistanceDamageMultiplier);
                dst.TakeDamage(kineticDamage * DistanceDamageMultiplier, electricDamage * DistanceDamageMultiplier);
            }
        }
        if (explosionEffectPrefab != null) {
            Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
            //explosionEffect.Play();
        }
        if (soundEffect != null) {
            var sfx = Instantiate(soundEffect, transform.position, transform.rotation);
            var audioSource = sfx.GetComponent<AudioSource>();
            audioSource.volume = GameManager.Instance.miscVolume;
            audioSource.Play();
            sfx.GetComponent<DelayedDestroyer>().StartCountdown();
        }
        
        killBullet();

    }

}