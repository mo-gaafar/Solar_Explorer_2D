using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunCollectible : GunCollectible
{
    float delay;
    private void Start()
    {
        GameObject Player=GameObject.Find("Player");
        gunController = Player.GetComponent< GunController > ();
        CollectibleSpawnOffset = new Vector2(3, 3);
        SearchRadius = 5;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.tag == "Player")
        {
            //first tell the gunController to spawn the other object
            spriteRenderer.enabled = false;
            Collider.enabled = false;
            TheGun = Instantiate(gun, transform.position, transform.rotation);
            TheGun.name = "LaserGun(Player)";

            gunController.PickupGunCollectible(this);
        }

    }




}
