using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Gun {

    private LineRenderer laserLine;

    public override void AddDamage(int Addend)
    {
        damage += Addend;
    }

    public override void Awake () {
        // Debug.Log ("LaserGun Awake");
        laserLine = GetComponent<LineRenderer> ();
    }

    public override void Start () {
        // Debug.Log ("LaserGun Start");
    }
    public override void Update () {
        // Debug.Log ("LaserGun Update");
    }

    public override void Shoot (Transform firingPoint) {
        // Debug.Log ("firingPoint: " + firingPoint.position.x + " " + firingPoint.position.y);
        if (Time.time - lastShotTime > shootingInterval) {
            // Debug.Log ("LaserGun Shoot");
            lastShotTime = Time.time;
            laserLine.enabled = true;
            laserLine.SetPosition (0, firingPoint.position);

            RaycastHit2D hit = Physics2D.Raycast (firingPoint.position, firingPoint.up, range);

            if (hit) {
                Debug.Log ("Hit: " + hit.collider.gameObject.name);
                laserLine.SetPosition (1, hit.point);

                Health healthComponent = hit.collider.gameObject.GetComponent<Health> ();
                healthComponent?.onHit.Invoke (damage * Time.deltaTime); //damage per second
                // if (hit.collider.gameObject.tag == "Enemy") {
                //     hit.collider.gameObject.GetComponent<Enemy> ().TakeDamage (damage);
                // }
            } else {
                laserLine.SetPosition (1, firingPoint.position + (firingPoint.up * range));
            }
        }

    }
    public override void StopShoot () {
        // Debug.Log ("LaserGun StopShoot");

        laserLine.enabled = false;
    }

    public override void Reload () {
        // Debug.Log ("LaserGun Reload");
    }

    public override void Clone (GameObject srcGun, GameObject dstGun) {
        // Debug.Log ("LaserGun Clone");
    }
}