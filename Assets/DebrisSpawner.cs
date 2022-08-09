using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{
    BoxCollider2D BuildingCollider;
    Vector2 StartPoint;
    Vector2 EndPoint;
    Vector2 Range1;
    Vector2 Range2;
    public List<GameObject> Debris;
    GameObject CurrentDebris;
    [SerializeField] float MinOffset=0.2f;
    [SerializeField] float MaxOffset=1;

    void Start()
    {
        BuildingCollider=GetComponent<BoxCollider2D>();
        RandomizeStart();
        RandomizeEnd();
    }

    // Update is called once per frame
    void Update()
    {
       //Logic for When to shoot
    }


    public void GenerateDebris()
    {
        for(int j = 0; j < Random.Range(1,3); j++)
        {
            for(int i=0; i< Debris.Count; i++)
            {
                RandomizeStart();
                RandomizeEnd();
                Debug.Log(StartPoint);
                //Debug.Log("Endpoint is " +EndPoint);
                CurrentDebris = Instantiate(Debris[i], StartPoint, transform.rotation);
                CurrentDebris.GetComponent<Debris>().SetStart_End(StartPoint, EndPoint);
            }
        }


    }

    //Randomize Start point inside the collider
    void RandomizeStart()
    {
        StartPoint = new Vector2(Random.Range(BuildingCollider.bounds.min.x, BuildingCollider.bounds.max.x)
        , Random.Range(BuildingCollider.bounds.min.y, BuildingCollider.bounds.max.y));
    }

    //El end 3awzeeno 0.2 b3d bounds el collider l8ayet 1
    void RandomizeEnd()
    {
        EndPoint = (Vector2)transform.position;
        Range1 = new Vector2(Random.Range(BuildingCollider.bounds.max.x+ MinOffset, BuildingCollider.bounds.max.x+ MaxOffset)
        , Random.Range(BuildingCollider.bounds.min.y- MaxOffset, BuildingCollider.bounds.max.y+ MaxOffset));

        //Randomly flip around the line of symmetry(center)
        if (Random.value > 0.5)
            Range1.x = EndPoint.x - (Range1.x - EndPoint.x);

        Range2 = new Vector2(Random.Range(BuildingCollider.bounds.min.x - MaxOffset, BuildingCollider.bounds.max.x + MaxOffset)
       , Random.Range(BuildingCollider.bounds.max.y + MinOffset, BuildingCollider.bounds.max.y + MaxOffset));

        if (Random.value > 0.5)
            Range2.y = EndPoint.y - (Range2.y - EndPoint.y);

        if (Random.value > 0.5)
        {
            Range1 = Range2;
        }

        EndPoint = Range1;

    }
}
