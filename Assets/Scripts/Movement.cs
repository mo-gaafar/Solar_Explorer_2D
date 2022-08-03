using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    
    //TODO: is there a better way to connect movement to maincontroller?
    // [SerializeField] private MainController mainController;
    // mainController = GetComponent<MainController> ();

    // public void LookAt (Vector2 target) {
    //     Vector2 direction = target - (Vector2) mainController.position;
    //     direction.Normalize ();
    //     float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
    //     transform.rotation = Quaternion.Euler (0, 0, angle - 90);
    // }

    // public void MoveTowards (Vector2 target, float verticalAxis) {
    //     Vector2 direction = target - (Vector2) mainController.position;
    //     direction *= verticalAxis;
    //     mainController.rb.AddForce (new Vector2 (direction.x * mainController.maxForce, direction.y * mainController.maxForce));
    // }
}