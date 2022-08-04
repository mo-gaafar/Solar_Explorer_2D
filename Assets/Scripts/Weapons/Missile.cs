using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile {

    public override void Setup (Vector2 direction) {
        this.direction = direction;
        transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy (gameObject, 5);
    }

    private void Awake()
    {
        Debug.Log(" Hello World");
    }
    public override void Update () {
        float moveSpeed = 10f;
        transform.position += (Vector3) direction * moveSpeed * Time.deltaTime;

        // how to detect hits???

        // transform.Translate (direction * Time.deltaTime);
    }

    public override void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            Debug.Log ("Hit enemy");

            // OnHit.Invoke (other.gameObject); // call OnHit event
            Health healthComponent = other.gameObject.GetComponent<Health> ();
            healthComponent?.onHit.Invoke (damage);

            // other.gameObject.GetComponent<Enemy> ().TakeDamage (damage);
            Destroy (gameObject);

        }
        if (other.gameObject.tag == "Obstacle") {
            // Debug.Log ("Hit wall");
            Destroy (gameObject);
        }
    }
}