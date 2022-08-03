using System.Collections.Generic;
using UnityEngine;

public class ChargerGoAroundState : ChargerBaseState {

    public override void EnterState (ChargerEnemyController controller) { }
    public override void UpdateState (ChargerEnemyController controller) {

        if (controller.PlayerDistance < controller.ATTACK_RANGE &&
            controller.AttackCounter < controller.AttackCooldown) {

            if (Time.time > controller.AttackTimeStart + controller.AttackTimeCooldown) {
                //Attack if there is no cooldown
                controller.SwitchState (controller.ChargerAttackState);
            } else {
                //Keep going around player in circles
                controller.CircleAround ();
            }
        } else {
            controller.SwitchState (controller.ChargerGoHomeState);
        }
    }

}