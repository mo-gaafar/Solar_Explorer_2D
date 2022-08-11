using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    //TODO: WE MIGHT CHANGE THIS IMPLEMENTATION LATER
    public GameObject HitEffect1;
    public GameObject HitEffect2;
    float bombtimer=5;
    float bombradius=5;
    Rigidbody2D rb;
    LayerMask Affectedlayers;
    public override void Setup(Vector2 direction)
    {
        this.direction = direction;

        rb.AddForce(this.direction*10, ForceMode2D.Impulse);
        
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
        Destroy(gameObject, bombtimer);
    }
    public override void Update()
    {
        //float moveSpeed = 10f;
        //transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

        // how to detect hits???

        // transform.Translate (direction * Time.deltaTime);
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player" || other.gameObject.tag == "Obstacle")
        //{
        //    //Debug.Log ("Hit enemy");

        //    // OnHit.Invoke (other.gameObject); // call OnHit event
        //    Health healthComponent = other.gameObject.GetComponent<Health>();
        //    healthComponent?.onHit.Invoke(damage);

        //    // other.gameObject.GetComponent<Enemy> ().TakeDamage (damage);

        //}
        //if (other.gameObject.tag == "Obstacle")
        //{
        //    // Debug.Log ("Hit wall");
        //}
        //gameObject.GetComponent<AudioSource>().Play();
        //GameObject lol = Instantiate(HitEffect1, transform.position, transform.rotation);
        //lol = Instantiate(HitEffect2, transform.position, transform.rotation);

        //Destroy(gameObject);

    }

    private void OnDestroy()
    {
        Collider2D[]  col = Physics2D.OverlapCircleAll(transform.position, bombradius, Affectedlayers);
        for(int i = 0; i < col.Length; i++)
        {
            if (col[i].gameObject.tag == "Enemy" || col[i].gameObject.tag == "Player" || col[i].gameObject.tag == "Obstacle")
            {
                Debug.Log("Hit enemy");

                // OnHit.Invoke (other.gameObject); // call OnHit event
                Health healthComponent = col[i].gameObject.GetComponent<Health>();
                healthComponent?.onHit.Invoke(damage);

                // other.gameObject.GetComponent<Enemy> ().TakeDamage (damage);

            }
        }
        //gameObject.GetComponent<AudioSource>().Play();
        GameObject lol = Instantiate(HitEffect1, transform.position, transform.rotation);
        //lol = Instantiate(HitEffect2, transform.position, transform.rotation);
    }

}
