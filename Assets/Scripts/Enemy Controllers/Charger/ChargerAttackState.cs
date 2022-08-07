using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerAttackState : ChargerBaseState {

    public override void EnterState (ChargerEnemyController controller) {
        controller.AttackTimeStart = Time.time;
        controller.AttackCounter++;
        Debug.Log ("Entering ChargerAttackState");
    }
    public override void UpdateState (ChargerEnemyController controller) {
        if (Time.time > controller.AttackTimeStart + controller.AttackTimeCooldown &&
            controller.AttackCounter < controller.AttackCooldown) {
            //Attack if there is no cooldown
            controller.LookAt (controller.Player.transform.position);
            controller.MoveTowards (controller.Player.transform.position, 2f);

        } else {
            //Keep going around player in circles
            controller.SwitchState (controller.ChargerGoAroundState);
        }
    }
    public void OnCollisionEnter2D (ChargerEnemyController controller, Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Health> ().onHit.Invoke (controller.DamagePerSecond * Time.deltaTime);
            controller.SwitchState (controller.ChargerGoAroundState);
            //TODO: Add a knockback effect to the player
            //TODO: what if the player dies??
        }
    }
}