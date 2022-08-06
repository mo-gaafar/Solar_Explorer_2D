using UnityEngine;

public class ChargerIdleState : ChargerBaseState {

    public override void EnterState (ChargerEnemyController controller) {
    }
    public override void UpdateState (ChargerEnemyController controller) {
        controller.StopMoving ();
        if (controller.PlayerDistance < controller.FOLLOW_PLAYER_RANGE) {
            controller.SwitchState (controller.ChargerGoToState);
        }
    }
}