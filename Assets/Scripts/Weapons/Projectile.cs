using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

abstract public class Projectile : MonoBehaviour {
    protected Vector2 direction;
    protected float damage;

    public void SetDamage (float damage) {
        this.damage = damage;
    }
    abstract public void Setup (Vector2 direction);
    abstract public void Update ();
    abstract public void OnCollisionEnter2D (Collision2D other);

}