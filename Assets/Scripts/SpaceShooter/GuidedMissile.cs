using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissile : MonoBehaviour {
    // Start is called before the first frame update

    public GameObject target;
    public float speed = 0.5f;
    public float turnSpeed = 10;
    public float lifetime = 10;
    public GameObject explosionEffectPrefab;
    public bool explodeOnLifeExpire = false;
    public float damage = 10;
    public float tolorateDist = 10;
    private float distance = -1;

    public bool missileReady = false;

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
            } else {
                distance = -1;
            }
        
            transform.position += transform.forward * speed;
        
            if ((lifetime <= 0 && explodeOnLifeExpire) || distance < tolorateDist) {
                explode();
            }
            if (lifetime <=  0) {
                killBullet();
            }
        }
    }
    
    public void killBullet() {
        Destroy(this.gameObject);
        Destroy(this);
    }
    
    public void explode() {

        // no Idea how u wanna implement the damage dealing thing but I know it should be here and u should use the "damage" variable.
        if (explosionEffectPrefab != null) {
            Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
            //explosionEffect.Play();
        }
        killBullet();

    }

}