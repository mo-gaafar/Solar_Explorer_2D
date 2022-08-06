using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyTurretController : MainController {

    [SerializeField] private Transform turretPivot;
    private GameObject targetPlayer;

    [SerializeField] private float TURRET_ATTACK_RANGE = 10f;
    [SerializeField] private float TURRET_TIME_COOLDOWN = 1f;
    private float TurretTimeStart = 0f;

    // Start is called before the first frame update
    void Start () {
        base.Start ();
        targetPlayer = GameObject.FindGameObjectWithTag ("Player");

        onNearbyEnemy.AddListener ((distance) => {
            TurretAttack (distance);
        });
    }

    // Update is called once per frame
    void Update () {

    }

    async void TurretAttack (float distance) {
        LookAt (targetPlayer.transform.position);
        if (distance < TURRET_ATTACK_RANGE) {
            onShoot.Invoke ();
            await Task.Delay ((int) TURRET_TIME_COOLDOWN * 1000);
            onStopShoot.Invoke ();
        }

        // if (Time.time > TurretTimeStart + TURRET_TIME_COOLDOWN) {
        //     if (distance < TURRET_ATTACK_RANGE) {
        //         onShoot.Invoke ();
        //     }
        // } else {
        //     TurretTimeStart = Time.time;
        //     onStopShoot.Invoke ();
        // }
    }

    void LookAt (Vector2 target) {
        Vector2 direction = target - (Vector2) turretPivot.position;
        direction.Normalize ();
        float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler (0, 0, angle + 90);
    }
}