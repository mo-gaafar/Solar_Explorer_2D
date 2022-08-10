using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class SoldierEnemyController : MainController {
    // * States
    public SoldierBaseState currentState;
    public SoldierPatrolState SoldierPatrolState = new SoldierPatrolState ();
    public SoldierAttackState SoldierAttackState = new SoldierAttackState ();

    // * Tunable Variables
    public float FOLLOW_PLAYER_RANGE = EnemyDetectionRadius * 0.9f;
    public float ATTACK_RANGE = EnemyDetectionRadius * 0.5f;
    public float STOP_RANGE = EnemyDetectionRadius * 0.1f;
    public float PATROL_RADIUS = 10f;

    [SerializeField] public float HitDamage = 10f;

    //! Important Prefabs
    [SerializeField] public GameObject _guntype;
    [SerializeField] public GameObject pfProjectile;
    // [SerializeField] public int AttackCooldown = 3;

    // * Private Variables
    [HideInInspector] public GameObject Player;
    [HideInInspector] public List<GameObject> AttackerQueue;
    [HideInInspector] public int MaxAttackers;

    // docs https://arongranberg.com/astar/docs/aipath.html
    [HideInInspector] public AIPath AIPath;
    [HideInInspector] public AIDestinationSetter Destination;

    [HideInInspector] public GunController GunController;
    [HideInInspector] public Vector2 HomePosition; //Spawn location
    [HideInInspector] public float PlayerDistance = 100f;
    // [HideInInspector] public float AttackTimeStart = 0f;
    // [HideInInspector] public float AttackCounter = 0f;
    // [HideInInspector] public float AttackTimeCooldown = 1f;

    [SerializeField] float CirclingRatePerSec = 5f;
    private float TempAngle = 0f;
    // Start is called before the first frame update
    void Start () {
        //TODO create a class for generic enemy
        base.Start ();
        //Find player in scene
        Player = GameObject.FindGameObjectWithTag ("Player");
        AttackerQueue = Player.GetComponent<PlayerController> ().EnemiesAttacking;
        MaxAttackers = Player.GetComponent<PlayerController> ().MaxEnemiesAttacking;
        HomePosition = gameObject.transform.position;
        GunController = GetComponent<GunController> ();
        AIPath = GetComponent<AIPath> ();
        Destination = GetComponent<AIDestinationSetter> ();
        // EnemyAI.

        onNearbyEnemy.AddListener ((distance) => {
            PlayerDistance = distance;
        });

        //! Intializing the gun and projectile
        GunController gc = GetComponent<GunController> ();
        GameObject gun = Instantiate (_guntype);
        if (gun.GetComponent<Gun> () as ProjectileGun) {
            ((ProjectileGun) gun.GetComponent<Gun> ()).SetProjectile (pfProjectile);
        }
        gc.SetGun (gun.GetComponent<Gun> ());

        //* The starting state is the patrol state

        currentState = SoldierPatrolState;
        currentState.EnterState (this);
    }

    // Update is called once per frame
    void Update () {
        // // Remove this asap
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

public class SoldierPatrolState : SoldierBaseState {
    private float RecalculateWaypointTimer = 0f;
    private float RecalculateWaypointTime = 4f;
    private float PrevPosition = 0f;
    public override void EnterState (SoldierEnemyController controller) {
        Debug.Log ("Entering SoldierPatrolState");
        controller.Destination.target = new GameObject ().transform;
        controller.Destination.target.position = GenerateRandomPoint (controller);
    }
    public override void UpdateState (SoldierEnemyController controller) {
        // if no players nearby keep patrolling within spawn radius 
        // Debug.Log ("PlayerDistance: " + controller.PlayerDistance);
        // Debug.Log ("AttackerQueue: " + controller.AttackerQueue.Count);
        // if player nearby and player enemy queue has space then switch to attack state

        if (controller.PlayerDistance < controller.FOLLOW_PLAYER_RANGE &&
            controller.AttackerQueue.Count < controller.MaxAttackers) {
            //add myself to the queue
            controller.AttackerQueue.Add (controller.gameObject);

            controller.SwitchState (controller.SoldierAttackState);
        }

        if (Vector2.Distance (controller.transform.position, controller.Destination.target.position) < controller.STOP_RANGE) {
            controller.Destination.target.position = GenerateRandomPoint (controller);
        }
        // checks if the waypoint is unreachable (enemy stuck in a corner)
        // by checking if x or y position has changed 
        // if (Mathf.Abs (controller.transform.position.x - PrevPosition) < 0.0001f) {
        //     PrevPosition = controller.transform.position.x;
        //     Debug.Log ($"Waypoint unreachable {PrevPosition}");
        //     // controller.rb.velocity = Vector2.zero;

        // }
        RecalculateWaypointTimer += Time.deltaTime;
        if (RecalculateWaypointTimer > RecalculateWaypointTime) {
            RecalculateWaypointTimer = 0f;
            controller.Destination.target.position = GenerateRandomPoint (controller);
        }

    }
    public void OnCollisionEnter2D (SoldierEnemyController controller, Collision2D other) {
        controller.Destination.target.position = GenerateRandomPoint (controller);
        //circlecast2d to check if something within range

    }

    public Vector2 GenerateRandomPoint (SoldierEnemyController controller) {
        // generate random point within patrol radius
        float randomAngle = Random.Range (0f, 360f);
        float randomRadius = Random.Range (controller.ATTACK_RANGE, controller.PATROL_RADIUS);
        Vector2 randomPoint = new Vector2 (randomRadius * Mathf.Cos (randomAngle), randomRadius * Mathf.Sin (randomAngle));
        randomPoint += controller.HomePosition;
        return randomPoint;
    }
}

public class SoldierAttackState : SoldierBaseState {
    // lets not overengineer this
    // proceeds to do just that:
    // private SoldierBaseState currentSubState;
    // private SoldierGoToState SoldierGoToState = new SoldierGoToState ();
    // private SoldierShootState SoldierShootState = new SoldierShootState ();

    public override void EnterState (SoldierEnemyController controller) {
        Debug.Log ("Entering SoldierAttackState");
    }
    public override void UpdateState (SoldierEnemyController controller) {
        switch (controller.PlayerDistance) {

            case float distance when distance < controller.STOP_RANGE:
                controller.AIPath.isStopped = true;
                controller.LookAt (controller.Player.transform.position);
                break;
            case float distance when distance < controller.ATTACK_RANGE:
                // controller.SwitchState (controller.SoldierShootState)
                //dont move and start shooting
                controller.Destination.target = controller.Player.transform;
                controller.LookAt (controller.Player.transform.position);
                controller.onShoot.Invoke ();

                break;
            case float distance when distance < controller.FOLLOW_PLAYER_RANGE:
                controller.Destination.target = controller.Player.transform;
                // move and stop shooting
                controller.AIPath.isStopped = false;

                // controller.SwitchState (controller.SoldierGoToState);
                break;
            case float distance when distance > controller.FOLLOW_PLAYER_RANGE:
                //remove myself from the queue
                controller.AttackerQueue.Remove (controller.gameObject);
                //go back to patrol state
                controller.SwitchState (controller.SoldierPatrolState);

                break;
            default:
                break;
        }
    }
}