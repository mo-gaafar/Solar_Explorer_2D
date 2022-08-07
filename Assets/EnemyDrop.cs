using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField]GameObject Slime;
    // Start is called before the first frame update
    public void GenerateSlime()
    {
        Instantiate(Slime,transform.position,Quaternion.identity);


    }
}
