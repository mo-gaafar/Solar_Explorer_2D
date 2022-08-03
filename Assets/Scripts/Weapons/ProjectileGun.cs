using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGun : Gun {

    [SerializeField] private Transform pfProjectile;

    public override void Awake () {
        Debug.Log ("ProjectileGun Awake");
    }

    public override void Start () {
        Debug.Log ("ProjectileGun Start");

    }
    public override void Update () {
        // Debug.Log ("ProjectileGun Update");

    }

    public override void Shoot (Transform firingPoint) {
        Debug.Log ("ProjectileGun Shoot");
        // Debug.Log ("firingPoint: " + firingPoint.position.x + " " + firingPoint.position.y);
        if (Time.time - lastShotTime > shootingInterval) {
            lastShotTime = Time.time;
            Transform projectileTransform = Transform.Instantiate (pfProjectile, firingPoint.position, firingPoint.rotation);
            Projectile projectile = projectileTransform.GetComponent<Projectile> ();
            projectile.Setup (projectileTransform.up);
            projectile.SetDamage (damage);
        }
    }

    public override void StopShoot () {
        Debug.Log ("ProjectileGun StopShoot");
    }

    public override void Reload () {
        Debug.Log ("ProjectileGun Reload");
    }

    //Deepcopy
    public override void Clone (GameObject srcGun, GameObject dstGun) {

    }
}