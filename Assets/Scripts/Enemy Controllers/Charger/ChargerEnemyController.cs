using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerEnemyController : MainController {

    public ChargerBaseState currentState;
    public ChargerIdleState ChargerIdleState = new ChargerIdleState ();
    public ChargerGoToState ChargerGoToState = new ChargerGoToState ();
    public ChargerGoAroundState ChargerGoAroundState = new ChargerGoAroundState ();
    public ChargerAttackState ChargerAttackState = new ChargerAttackState ();
    public ChargerGoHomeState ChargerGoHomeState = new ChargerGoHomeState ();

    public float FOLLOW_PLAYER_RANGE = EnemyDetectionRadius * 0.9f;
    public float ATTACK_RANGE = EnemyDetectionRadius * 0.5f;
    [SerializeField] public float DamagePerSecond = 10f;
    [SerializeField] public int AttackCooldown = 3;

    [HideInInspector] public GameObject Player;
    [HideInInspector] public Vector2 HomePosition;
    [HideInInspector] public float PlayerDistance = 100f;
    [HideInInspector] public float AttackTimeStart = 0f;
    public float AttackCounter = 0f;
    public float AttackTimeCooldown = 1f;

    [SerializeField] float CirclingRatePerSec = 5f;
    private float TempAngle = 0f;

    // Start is called before the first frame update
    void Start () {
        base.Start ();
        //Find player in scene
        Player = GameObject.FindGameObjectWithTag ("Player");
        HomePosition = gameObject.transform.position;

        onNearbyEnemy.AddListener ((distance) => {
            PlayerDistance = distance;
        });

        //The starting state is the idle state
        currentState = ChargerIdleState;
        //reference to this script is passed as argument
        currentState.EnterState (this);
    }

    // Update is called once per frame
    void Update () {
        currentState.UpdateState (this);
    }

    public void SwitchState (ChargerBaseState newState) {
        currentState = newState;
        currentState.EnterState (this);
    }

    public void CircleAround () {
        Vector2 circleCenter = Player.transform.position;
        TempAngle += Time.deltaTime * 3.14f * 2 * CirclingRatePerSec;
        if (TempAngle > 360f)
            TempAngle = 0f;

        Vector2 rotatingVect = new Vector2 (Mathf.Sin (TempAngle), Mathf.Cos (TempAngle)) * EnemyDetectionRadius;
        Vector2 posOnCircle = circleCenter + rotatingVect;
        MoveTowards (posOnCircle, 1f);
        LookAt (posOnCircle);
    }

    public void OnCollisionEnter2D (Collision2D other) {
        //quick fix for damage per second
        if (other.gameObject.tag == "Player") {
            other.collider.GetComponent<Health> ().onHit.Invoke (DamagePerSecond * Time.deltaTime);
        }
        currentState.OnCollisionEnter2D (this, other);
    }
}