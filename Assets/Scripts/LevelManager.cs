using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public float MaxLevelTimeSec = 100f;
    private float currentLevelTime;
    private bool isLevelOver = false;
    private int CurrentLivingEnemies = 0;
    private int MaxNumberOfEnemies = 10;
    public UnityEvent onLevelOver;
    public UnityEvent onLevelStart;
    public GameObject Enemyspawner;

    // Update is called once per frame
    void Update () {

        currentLevelTime += Time.deltaTime;
        if (currentLevelTime >= MaxLevelTimeSec) {
            currentLevelTime = 0;
            // SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
        }

    }

    public string GetLevelTimeString () {
        int remaningTime = (int) (MaxLevelTimeSec - currentLevelTime);
        int minutes = (int) (remaningTime / 60);
        int seconds = (int) (remaningTime % 60);
        return string.Format ("{0:00}:{1:00}", minutes, seconds);
    }

    public void DecreaseLivingEnemies () {
        CurrentLivingEnemies--;
        (Enemyspawner.GetComponent<EnemySpawner> ()).DecreaseCounter ();
    }

    // public void GameOver(){
    //     isLevelOver = true;
    //     onLevelOver.Invoke();
    // }
    public void Retry () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
    }

}