using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    float Timer=0;
    public float  AnimationTime=1;
    float  SlidingTimeFactor=5;
    bool Project = true;

    //Trajectory
    public Vector2 StartPoint;
    public Vector2 TrajEndPoint;

    //Sliding
    public Vector2 FinalEndpoint;
    bool Slide=false;
    float RotationSpeed = 2;
    float RotationJerk = 5;
    public float Force=3;

    SpriteRenderer Sr;
    public void SetStart_End(Vector2 Vec,Vector2 Vec2)
    {
        StartPoint = Vec;
        TrajEndPoint = Vec2;
        //Debug.Log("TrajEnd is " + TrajEndPoint);
    }



    void Start()
    {
        RotationSpeed = Random.Range(2, 4);
        RotationJerk = Random.Range(5, 10);
        Force = Random.Range(1, 6);
        Sr=GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Don't forget to rotate
        Timer += Time.deltaTime;
        if (Project)
        {
            transform.Rotate(0, 0, RotationSpeed);
            //Timer = Timer % AnimationTime;
            //animation time is 2 seconds
            if (Mathf.Abs(AnimationTime - Timer) <= 0.02)
            {
                //Debug.Log("LolDEBRISSSSSSS");
                Project = false;
                Slide = true;
                Timer = 0;
            }
            // Timer/AnimationTime because it needs to be normalized for the Vector.Lerp function inside.
            transform.position = MathParabola.Parabola(StartPoint, TrajEndPoint, 1, Timer / AnimationTime);

        }
        else
        {
            if (Slide)
            {
                //Debug.Log("Sliding");
                //Increase Rotation Considerably
                transform.Rotate(0, 0, RotationSpeed+ RotationJerk);
                //slide then fade into destruction
                //get direction of trajectory and move for a bit
                Vector2 direction = TrajEndPoint - StartPoint;
                FinalEndpoint = TrajEndPoint + direction.normalized * Random.Range(0.1f, 0.2f);
                transform.position = TrajEndPoint + Timer*SlidingTimeFactor * (FinalEndpoint - TrajEndPoint);
                if (Vector2.SqrMagnitude((Vector2)transform.position - FinalEndpoint) <= 0.0001)
                {
                    Slide = false;
                    Timer = 0;                }
                ///FOR DEBUGGING 
                //Project = true;
                //Timer = 0;
            }
            else
            {
                //Fade
                Sr.color= new Color (Sr.color.r,Sr.color.g,Sr.color.b,Timer);
                if(Mathf.Abs(1 - Timer) <= 0.02)
                {
                    Destroy(gameObject);
                }
            }
        }
    }


}
