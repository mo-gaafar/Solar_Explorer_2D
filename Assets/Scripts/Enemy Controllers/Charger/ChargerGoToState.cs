using UnityEngine;

public class ChargerGoToState : ChargerBaseState {
    public override void EnterState (ChargerEnemyController controller) {
        Debug.Log ("Entering ChargerGoToState");
    }
    public override void UpdateState (ChargerEnemyController controller) {
        controller.MoveTowards (controller.Player.transform.position, 2f);
        controller.LookAt (controller.Player.transform.position);

        if (controller.PlayerDistance < controller.ATTACK_RANGE) {
            controller.SwitchState (controller.ChargerGoAroundState);
        }
    }
}