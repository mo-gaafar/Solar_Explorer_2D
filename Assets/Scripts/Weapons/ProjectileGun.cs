using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGun : Gun {

    [SerializeField] private GameObject pfProjectile;
    [SerializeField] private int NumberOfProjectiles=1;
    [SerializeField] private float gunWidth=2f;
    [SerializeField] private List<GameObject> BulletPrefabs;
    [SerializeField] private List<GameObject> MissilePrefabs;
    [SerializeField] private List<GameObject> BombPrefabs;

    public void AddtoNumberProjectiles(int Addend)
    {
        NumberOfProjectiles += Addend;
    }

    void SetBulletPrefab()
    {
        if (damage > 15)
        {
            pfProjectile = BulletPrefabs[1];


        }
        if (damage > 20)
        {
            pfProjectile = BulletPrefabs[2];
        }
    }
    void SetMissilePrefab()
    {
        if (damage > 25)
        {
            pfProjectile = MissilePrefabs[1];


        }
        if (damage > 35)
        {
            pfProjectile = MissilePrefabs[2];
        }
    }

    void SetBombPrefab()
    {
        if (damage > 40)
        {
            pfProjectile = BombPrefabs[1];


        }
        if (damage > 50)
        {
            pfProjectile = BombPrefabs[2];
        }
    }

    public override void AddDamage(int Addend)
    {
        damage += Addend;
        if (pfProjectile.GetComponent<Bullet>()) { SetBulletPrefab(); }
        if (pfProjectile.GetComponent<Missile>()) { SetMissilePrefab(); }
        if (pfProjectile.GetComponent<Bomb>()) { SetBombPrefab(); }
    }
    public void SetProjectile(GameObject newProjectile)
    {
        pfProjectile = newProjectile;
        if (pfProjectile?.GetComponent<Bullet>())
        {
            damage = 10;
        }
        else if (pfProjectile?.GetComponent<Missile>())
        {
            damage = 20;
        }else if (pfProjectile?.GetComponent<Bomb>())
        {
            damage = 35;
        }
    }

    public override void Awake () {
        //Debug.Log ("ProjectileGun Awake");
        

    }

    public override void Start () {
        //Debug.Log ("ProjectileGun Start");
        if (pfProjectile?.GetComponent<Bullet>())
        {
            damage = 10;
        }
        else if (pfProjectile?.GetComponent<Missile>())
        {
            damage = 20;
        }
        else if (pfProjectile?.GetComponent<Bomb>())
        {
            damage = 35;
        }

    }
    public override void Update () {
        // Debug.Log ("ProjectileGun Update");

    }

    public override void Shoot(Transform firingPoint)
    {
        //Debug.Log("ProjectileGun Shoot");

        if (Time.time - lastShotTime > shootingInterval)
        {
            //Debug.Log("Shoot mn gowa");
            lastShotTime = Time.time;
            if (NumberOfProjectiles == 1)
            {
                //Debug.Log("Shootsingle barra");
                ShootSingle(firingPoint.position, firingPoint.rotation);
            }
            else
            {
                Vector2 Offset = new Vector2(0, 0);
                float magnitude = 0;
                gunWidth = NumberOfProjectiles - 0.5f;
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

                if (i % 2 == 0)
                {
                    ShootSingle(firingPoint.position, firingPoint.rotation);
                }
                else
                {
                    //magnitude += increment;
                    Offset = -magnitude * (firingPoint.right);
                    ShootSingle(firingPoint.position + (Vector3)Offset, firingPoint.rotation);
                }

            }

        }
    }

    void ShootSingle(Vector3 Position, Quaternion rotation)
    {
       
        GameObject projectile = Instantiate(pfProjectile, Position, rotation);
        if (projectile.GetComponent<Projectile>())
        {
        Projectile projectileScript = projectile.GetComponent<Projectile>();
       
        projectileScript.Setup(projectile.transform.up);
        projectileScript.SetDamage(damage);

        }
    }

    public override void StopShoot () {
        
    }

    public override void Reload () {
        Debug.Log ("ProjectileGun Reload");
    }

    //Deepcopy
    public override void Clone (GameObject srcGun, GameObject dstGun) {

    }
}