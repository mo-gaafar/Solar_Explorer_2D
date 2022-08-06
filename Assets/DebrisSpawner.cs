using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{
    BoxCollider2D BuildingCollider;
    Vector2 StartPoint;
    Vector2 EndPoint;
    Vector2 Offset1;
    Vector2 Offset2;
    public List<GameObject> Debris;
    GameObject CurrentDebris;

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
                Debug.Log("Endpoint is " +EndPoint);
                CurrentDebris = Instantiate(Debris[i], StartPoint, transform.rotation);
                CurrentDebris.GetComponent<Debris>().SetStart_End(StartPoint, EndPoint);
            }
        }


    }

    //Randomize Start point inside the collider
    void RandomizeStart()
    {
        StartPoint = (Vector2)transform.position
        + new Vector2(Random.Range(BuildingCollider.bounds.min.x, BuildingCollider.bounds.max.x) - transform.position.x
        , Random.Range(BuildingCollider.bounds.min.y, BuildingCollider.bounds.max.y) - transform.position.y);
    }

    //El end 3awzeeno 0.2 b3d bounds el collider l8ayet 1
    void RandomizeEnd()
    {
        EndPoint = (Vector2)transform.position;
        Offset1 = new Vector2(Random.Range(BuildingCollider.bounds.max.x+0.2f, BuildingCollider.bounds.max.x+1f)
        , Random.Range(BuildingCollider.bounds.min.y-1f, BuildingCollider.bounds.max.y+1f));

        //Randomly flip around the line of symmetry (center)
        if (Random.value > 0.5) 
            Offset1.x = EndPoint.x - (Offset1.x- EndPoint.x);
        
        Offset2 = new Vector2(Random.Range(BuildingCollider.bounds.min.x - 1f, BuildingCollider.bounds.max.x + 1f)
       , Random.Range(BuildingCollider.bounds.max.y + 0.2f, BuildingCollider.bounds.max.y + 1f));

         if (Random.value > 0.5) 
            Offset2.y = EndPoint.y -(Offset2.y - EndPoint.y);
        
        if(Random.value > 0.5)
        {
            Offset1 = Offset2;
        }

        EndPoint = EndPoint + Offset1;

    }
}
