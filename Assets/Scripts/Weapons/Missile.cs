using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile {

    //TODO: WE MIGHT CHANGE THIS IMPLEMENTATION LATER
    public GameObject HitEffect1;
    public GameObject HitEffect2;
    public override void Setup (Vector2 direction) {
        this.direction = direction;
        //transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy (gameObject, 5);
    }

    private void Awake () {

    }
    public override void Update () {
        float moveSpeed = 10f;
        transform.position += (Vector3) direction * moveSpeed * Time.deltaTime;

        // how to detect hits???

        // transform.Translate (direction * Time.deltaTime);
    }

    public override void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player" || other.gameObject.tag == "Obstacle") {
            Debug.Log ("Hit enemy");

            // OnHit.Invoke (other.gameObject); // call OnHit event
            Health healthComponent = other.gameObject.GetComponent<Health> ();
            healthComponent?.onHit.Invoke (damage);

            // other.gameObject.GetComponent<Enemy> ().TakeDamage (damage);

        }
        if (other.gameObject.tag == "Obstacle") {
            // Debug.Log ("Hit wall");
        }

        GameObject lol = Instantiate (HitEffect1, transform.position, transform.rotation);
        lol = Instantiate (HitEffect2, transform.position, transform.rotation);

        Destroy (gameObject);

    }
}