using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Upgrade
{
    [SerializeField] float AddedMaximumHealth = 50f;
    Health healthcomp;
    private void Start()
    {
        GameObject Player = GameObject.Find("Player");
        healthcomp = Player.GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Upgrade();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Sr.enabled == true)
        {
            Sr.color = new Color(Sr.color.r, Sr.color.g, Sr.color.b, 1f);
        }
    }

    void Upgrade()
    {
        healthcomp.AddMaxHealth(AddedMaximumHealth);
        Destroy(gameObject);

    }
}
