using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierEnemyController : MainController {
    // * States
    public SoldierBaseState currentState;
    public SoldierPatrolState SoldierPatrolState = new SoldierPatrolState ();
    public SoldierAttackState SoldierAttackState = new SoldierAttackState ();
    public SoldierIdleState SoldierIdleState = new SoldierIdleState ();
    public SoldierGoHomeState SoldierGoHomeState = new SoldierGoHomeState ();
    public SoldierGoToState SoldierGoToState = new SoldierGoToState ();

    // * Tunable Variables
    public float FOLLOW_PLAYER_RANGE = EnemyDetectionRadius * 0.9f;
    public float ATTACK_RANGE = EnemyDetectionRadius * 0.5f;
    [SerializeField] public float HitDamage = 10f;
    [SerializeField] public int AttackCooldown = 3;

    // * Private Variables
    [HideInInspector] public GameObject Player;
    [HideInInspector] public GunController GunController;
    [HideInInspector] public Vector2 HomePosition;
    [HideInInspector] public float PlayerDistance = 100f;
    [HideInInspector] public float AttackTimeStart = 0f;
    [HideInInspector] public float AttackCounter = 0f;
    [HideInInspector] public float AttackTimeCooldown = 1f;

    [SerializeField] float CirclingRatePerSec = 5f;
    private float TempAngle = 0f;
    // Start is called before the first frame update
    void Start () {
        //TODO create a class for generic enemy
        base.Start ();
        //Find player in scene
        Player = GameObject.FindGameObjectWithTag ("Player");
        HomePosition = gameObject.transform.position;
        GunController = GetComponent<GunController> ();

        onNearbyEnemy.AddListener ((distance) => {
            PlayerDistance = distance;
        });

        //The starting state is the patrol state

        currentState = SoldierPatrolState;
        currentState.EnterState (this);
    }

    // Update is called once per frame
    void Update () {
        //TODO Remove this asap
        // LookAt (Player.transform.position);
        currentState.UpdateState (this);
    }
    public void SwitchState (SoldierBaseState newState) {
        currentState = newState;
        currentState.EnterState (this);
    }

    public void OnCollisionEnter2D (Collision2D other) {
        //quick fix for damage per second
        if (other.gameObject.tag == "Player") {
            other.collider.GetComponent<Health> ().onHit.Invoke (HitDamage * Time.deltaTime);
        }
        currentState.OnCollisionEnter2D (this, other);
    }
}

public abstract class SoldierBaseState {
    public abstract void EnterState (SoldierEnemyController chargerStateManager);
    public abstract void UpdateState (SoldierEnemyController chargerStateManager);
    public void OnCollisionEnter2D (SoldierEnemyController controller, Collision2D other) { }
}

public class SoldierIdleState : SoldierBaseState {
    public override void EnterState (SoldierEnemyController controller) {
        Debug.Log ("Entering SoldierIdleState");
    }
    public override void UpdateState (SoldierEnemyController controller) {
        // if (controller.PlayerDistance < controller.EnemyDetectionRadius) {
        //     controller.SwitchState (controller.SoldierPatrolState);
        // }
    }
}

public class SoldierPatrolState : SoldierBaseState {
    public override void EnterState (SoldierEnemyController controller) {
        Debug.Log ("Entering SoldierPatrolState");
    }
    public override void UpdateState (SoldierEnemyController controller) {
        if (controller.PlayerDistance < controller.FOLLOW_PLAYER_RANGE) {
            controller.SwitchState (controller.SoldierGoToState);
        }
    }
}

public class SoldierAttackState : SoldierBaseState {
    public override void EnterState (SoldierEnemyController controller) {
        Debug.Log ("Entering SoldierAttackState");
    }
    public override void UpdateState (SoldierEnemyController controller) {
        if (controller.PlayerDistance > controller.ATTACK_RANGE) {
            controller.SwitchState (controller.SoldierPatrolState);
        }
    }
}

public class SoldierGoHomeState : SoldierBaseState {
    public override void EnterState (SoldierEnemyController controller) {
        Debug.Log ("Entering SoldierGoHomeState");
    }
    public override void UpdateState (SoldierEnemyController controller) {
        if (controller.PlayerDistance > controller.FOLLOW_PLAYER_RANGE) {
            controller.SwitchState (controller.SoldierPatrolState);
        }
    }
}

public class SoldierGoToState : SoldierBaseState {
    public override void EnterState (SoldierEnemyController controller) {
        Debug.Log ("Entering SoldierGoToState");
    }
    public override void UpdateState (SoldierEnemyController controller) {
        // controller.MoveTowards (controller.Player.transform.position, 2f);
        // controller.LookAt (controller.Player.transform.position);

        if (controller.PlayerDistance < controller.ATTACK_RANGE) {
            controller.SwitchState (controller.SoldierAttackState);
        }
    }

}