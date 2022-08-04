using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Gun : MonoBehaviour {

    public enum ShootState {
        Idle,
        Shooting,
        Reloading
    }
    public ShootState shootState = ShootState.Idle;

    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float shootingInterval = 3f;
    [SerializeField] protected float range = 100f;
    [SerializeField] protected float lastShotTime = 0f;
    [SerializeField] protected float impactForce = 30f;

    // [Header("More Gun Properties :(")]
    // protected float reloadTime = 1f;
    // protected float reloadTimeLeft;
    // protected int maxAmmo = 10;
    // protected int maxClip = 10;
    // protected int currentAmmo;
    // protected int currentClip;
    // protected float fireTimeLeft;
    // protected bool isReloading = false;
    // protected bool isAutomatic = false;

    public abstract void Awake ();

    public abstract void Start ();
    public abstract void Update ();

    public abstract void Shoot (Transform firingPoint);
    public abstract void StopShoot ();

    public abstract void Reload ();

    public abstract void Clone (GameObject srcGun, GameObject dstGun);

}