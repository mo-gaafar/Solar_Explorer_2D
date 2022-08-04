using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGun : Gun {

    [SerializeField] private GameObject pfProjectile;

    public void SetProjectile(GameObject newProjectile)
    {
        pfProjectile = newProjectile;
    }

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
        Debug.Log (Time.time - lastShotTime);
        Debug.Log($"Shooting Interval is {shootingInterval}");

        if (Time.time - lastShotTime > shootingInterval) {
            lastShotTime = Time.time;
            Debug.Log("I am not sus ya nasser");
            GameObject projectile = Instantiate (pfProjectile, firingPoint.position, firingPoint.rotation);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.Setup (projectile.transform.up);
            projectileScript.SetDamage (damage);
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