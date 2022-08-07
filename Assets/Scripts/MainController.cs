using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Movement;

public abstract class MainController : MonoBehaviour {

    // protected Movement movement = new Movement ();

    public Health healthComponent;

    [SerializeField] public float maxForce = 5f;
    [SerializeField] public const float EnemyDetectionRadius = 10f;

    protected Vector2 position = new Vector2 ();
    // protected GunController gunController;
    protected Rigidbody2D rb;

    public UnityEvent onShoot;
    public UnityEvent onStopShoot;
    public UnityEvent onReload;
    public UnityEvent onSwitchWeapon;
    public UnityEvent<float> onNearbyEnemy;

    // Start is called before the first frame update
    protected void Start () {

        rb = GetComponent<Rigidbody2D> ();
        healthComponent = GetComponent<Health> ();
        // gunController = GetComponent<GunController> ();
        healthComponent?.onHit.AddListener ((damage) => {
            HitReaction ();
        });
    }

    // Update is called once per frame
    protected void Update () {

    }

    protected void FixedUpdate () {

        //Refresh position
        position = (Vector2) transform.position;
    }

    //movement 

    //TODO: add rotation offset arg?
    public void LookAt (Vector2 target) {
        Vector2 direction = target - (Vector2) position;
        direction.Normalize ();
        float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler (0, 0, angle - 90);
    }

    public void MoveTowards (Vector2 target, float verticalAxis) {
        Vector2 direction = target - (Vector2) position;
        direction *= verticalAxis;
        rb.AddForce (new Vector2 (direction.x * maxForce, direction.y * maxForce));
    }

    //health

    public void HitReaction () {
        onStopShoot.Invoke ();
        // onReload.Invoke ();

    }

    public void StopMoving () {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

}