using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour {
    public float MaxLevelTimeSec = 100f;
    private float currentLevelTime;
    private bool isLevelOver = false;
    public UnityEvent onLevelOver;
    public UnityEvent onLevelStart;

    // Update is called once per frame
    void Update () {

        currentLevelTime += Time.deltaTime;
        if (currentLevelTime >= MaxLevelTimeSec) {
            currentLevelTime = 0;
            // SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
        }

    }

    public string GetLevelTimeString () {
        int minutes = (int) (currentLevelTime / 60);
        int seconds = (int) (currentLevelTime % 60);
        return string.Format ("{0:00}:{1:00}", minutes, seconds);
    }
}