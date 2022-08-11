using UnityEngine;

public class ChargerGoHomeState : ChargerBaseState {
    public override void EnterState (ChargerEnemyController controller) {
        //Debug.Log ("Entering ChargerGoHomeState");
        controller.AttackCounter = 0;
    }
    public override void UpdateState (ChargerEnemyController controller) {
        float distanceToHome = Vector3.Distance (controller.transform.position, controller.HomePosition);
        if (controller.PlayerDistance > controller.FOLLOW_PLAYER_RANGE && distanceToHome < 0.4f) {
            controller.SwitchState (controller.ChargerIdleState);
        } else if (controller.AttackTimeStart + controller.AttackTimeCooldown < Time.time &&
            controller.PlayerDistance < controller.ATTACK_RANGE) {
            controller.SwitchState (controller.ChargerGoAroundState);
        } else {
            // Debug.Log ("homepos" + controller.HomePosition);
            controller.MoveTowards (controller.HomePosition, 1.5f);
            controller.LookAt (controller.HomePosition);
        }
    }
}