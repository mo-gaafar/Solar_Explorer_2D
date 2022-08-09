using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGun : Gun {

    [SerializeField] private GameObject pfProjectile;
    [SerializeField] private int NumberOfProjectiles=1;
    [SerializeField] private float gunWidth=2f;
    [SerializeField] private List<GameObject> BulletPrefabs;

    public void AddtoNumberProjectiles(int Addend)
    {
        NumberOfProjectiles += Addend;
    }

    public override void AddDamage(int Addend)
    {
        damage += Addend;
        if(damage > 10)
        {
            pfProjectile = BulletPrefabs[1];


        }
        if (damage > 20)
        {
            pfProjectile = BulletPrefabs[2];
        }
    }
    public void SetProjectile(GameObject newProjectile)
    {
        pfProjectile = newProjectile;
    }

    public override void Awake () {
        Debug.Log ("ProjectileGun Awake");
        if(pfProjectile.GetComponent<Bullet>())
        {
            damage = 10;
        }else if (pfProjectile.GetComponent<Missile>())
        {
            damage = 20;
        }

    }

    public override void Start () {
        Debug.Log ("ProjectileGun Start");

    }
    public override void Update () {
        // Debug.Log ("ProjectileGun Update");

    }

    public override void Shoot (Transform firingPoint) {
        //Debug.Log("ProjectileGun Shoot");

        if (Time.time - lastShotTime > shootingInterval) {
            //Debug.Log("Shoot mn gowa");
            lastShotTime = Time.time;
            if (NumberOfProjectiles == 1)
            {
            //Debug.Log("Shootsingle barra");
                ShootSingle(firingPoint.position, firingPoint.rotation);
            }
            else if (NumberOfProjectiles == 2)
            {
                Vector2 Offset = new Vector2(0, 0);
                float magnitude = 0;
                gunWidth = 1f;
                float increment = gunWidth / NumberOfProjectiles;
                for (int i = 0; i < 2; i++)
                {
                    if (i % 2 == 0)
                    {
                        magnitude += increment;
                        Offset = magnitude * (firingPoint.right);
                    }
                    else
                    {
                        Offset = -magnitude * (firingPoint.right);

                    }

                    ShootSingle(firingPoint.position + (Vector3)Offset, firingPoint.rotation);
                }
            }
            else
            {
                Vector2 Offset = new Vector2(0, 0);
                float magnitude = 0;
                gunWidth = NumberOfProjectiles-0.5f;
                float increment = gunWidth / NumberOfProjectiles;
                int i;
                for (i = 0; i < NumberOfProjectiles - 1; i++)
                {
                    if (i % 2 == 0)
                    {
                        magnitude += increment;
                        Offset = magnitude * (firingPoint.right);
                    }
                    else
                    {
                        Offset = -magnitude * (firingPoint.right);

                    }

                    ShootSingle(firingPoint.position + (Vector3)Offset, firingPoint.rotation);
                }

                if (i % 2 == 1)
                {
                    ShootSingle(firingPoint.position, firingPoint.rotation);
                }
                else
                {
                    magnitude += increment;
                    Offset = magnitude * (firingPoint.right);
                    ShootSingle(firingPoint.position + (Vector3)Offset, firingPoint.rotation);
                }


            }

        }
    }

    void ShootSingle(Vector3 Position, Quaternion rotation)
    {
        //Debug.Log("Shootsingle mn gowa");
        Debug.Log($"Euler mn barra is {rotation.eulerAngles.z}");
        GameObject projectile = Instantiate(pfProjectile, Position, rotation);
        //GameObject projectile = Instantiate(pfProjectile, Position, rotation);
        if (projectile.GetComponent<Projectile>())
        {
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        Debug.Log($"transform.up mn b3d elsetup is {projectile.transform.up}");
        projectileScript.Setup(projectile.transform.up);
        projectileScript.SetDamage(damage);

        }
        else
        {
            Debug.Log("3leek wa7ed yaba4a <3");
        }
    }

    public override void StopShoot () {
        //Debug.Log ("ProjectileGun StopShoot");
    }

    public override void Reload () {
        Debug.Log ("ProjectileGun Reload");
    }

    //Deepcopy
    public override void Clone (GameObject srcGun, GameObject dstGun) {

    }
}