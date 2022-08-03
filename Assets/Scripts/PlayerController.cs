using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MainController {

    [SerializeField] private Camera playerCamera;

    [SerializeField] private float PickupDetectionRadius = 1f;

    private float vertical = 0; //acc = 1 decel = -1

    // Update is called once per frame
    void Update () {
        //Calling the base class Update() and then extending it
        base.Update ();

        if (Input.GetButton ("Fire1")) {
            // Debug.Log ("Fire1");
            onShoot.Invoke ();
        }
        if (Input.GetButtonUp ("Fire1")) {
            Debug.Log ("StopFire1");
            onStopShoot.Invoke ();
        }
        if (Input.GetButtonDown ("Jump")) {
            Debug.Log ("Reload");
            onReload.Invoke ();
        }
        if (Input.GetButtonDown ("Fire3")) {
            Debug.Log ("SwitchWeapon");
            onSwitchWeapon.Invoke ();
        }

        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll (transform.position, EnemyDetectionRadius, LayerMask.GetMask ("Enemy"));
        if (nearbyEnemies != null && nearbyEnemies.Length > 0) {
            // Debug.Log ("Length: " + nearbyEnemies.Length);
            for (int i = 0; i < nearbyEnemies.Length; i++) {
                //TODO: is there a better way to do this?
                //check if main controller exists in enemy 
                //then invoke in nearby enemies
                MainController nearbyEnemy = nearbyEnemies[i].GetComponent<MainController> ();
                if (nearbyEnemy) {
                    float distance = Vector2.Distance (transform.position, nearbyEnemy.transform.position);
                    nearbyEnemy?.onNearbyEnemy.Invoke (distance);
                }
            }
        }
        Collider2D[] nearbyPickups = Physics2D.OverlapCircleAll (transform.position, EnemyDetectionRadius, LayerMask.GetMask ("Pickup"));
        if (nearbyPickups != null && nearbyPickups.Length > 0) {
            // Debug.Log ("Length: " + nearbyEnemies.Length);
            for (int i = 0; i < nearbyPickups.Length; i++) {
                Pickup nearbyPickup = nearbyPickups[i].GetComponent<Pickup> ();
                nearbyPickup?.onPickup.Invoke (gameObject);

            }
        }
    }

    private void FixedUpdate () {
        base.FixedUpdate ();

        vertical = Input.GetAxisRaw ("Vertical");
        Vector2 mousePos = playerCamera.ScreenToWorldPoint (Input.mousePosition);

        // TODO: use events and callbacks instead
        base.MoveTowards (mousePos, vertical);
        base.LookAt (mousePos);

    }
}