using UnityEngine;

public class ChargerIdleState : ChargerBaseState {

    public override void EnterState (ChargerEnemyController controller) {
        Debug.Log ("Entering ChargerIdleState");
    }
    public override void UpdateState (ChargerEnemyController controller) {
        controller.StopMoving ();
        // Debug.Log ("PlayerDistance: " + controller.PlayerDistance);
        if (controller.PlayerDistance < controller.FOLLOW_PLAYER_RANGE) {
            controller.SwitchState (controller.ChargerGoToState);
        }
    }
}