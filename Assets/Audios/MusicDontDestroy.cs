using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDontDestroy : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] musobjs = GameObject.FindGameObjectsWithTag("Game BGMusic");
        if(musobjs.Length >1)
        {
            Destroy(this.gameObject);    
        }
        DontDestroyOnLoad(this.gameObject);
    }
    
    

}
