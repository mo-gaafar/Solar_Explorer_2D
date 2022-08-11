using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public float MaxLevelTimeSec = 100f;
    private float currentLevelTime;
    private bool isLevelOver = false;
    [SerializeField] private bool TimeConstraintActive = true;
    private int CurrentLivingEnemies = 0;
    private int MaxNumberOfEnemies = 10;
    public UnityEvent onLevelOver;
    public UnityEvent onLevelStart;
    public GameObject Enemyspawner;
    public GameObject Player;
    public GameObject LevelOverScreen;
    public GameObject HUD;

    [SerializeField] List<Objective> MainObjectives;
    [SerializeField] List<Objective> SecondaryObjectives;

    // Update is called once per frame
    void Update () {

        currentLevelTime += Time.deltaTime;
        if (TimeConstraintActive && currentLevelTime >= MaxLevelTimeSec ) {
            currentLevelTime = 0;
            GameOver ();
            // onLevelOver.Invoke ();
            // SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
        }

    }

    public void SetTimeConstraintOFF()
    {
        TimeConstraintActive = false;
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
        UpdateandCheckEnemies();
    }

    public void GameOver () {
        // isLevelOver = true;
        Player.SetActive (false);
        LevelOverScreen.SetActive (true);
        HUD.SetActive (false);
        onLevelOver.Invoke ();
    }
    public void Retry () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
    }
    public void BackToMainMenu () {
        SceneManager.LoadScene (0);
    }

    public void UpdateandCheckFuel(float fraction)
    {
        for (int i = 0; i < MainObjectives.Count; i++)
        {
            if (MainObjectives[i].Type == ObjectiveType.Fuel)
            {
                Debug.Log(fraction);
                MainObjectives[i].CurrentCount = MainObjectives[i].requiredCount* fraction;
                if (MainObjectives[i].CheckComplete())
                {
                    Destroy(MainObjectives[i]);
                    MainObjectives.RemoveAt(i);

                }
            }

        }
        CheckLevelDone();
        for (int i = 0; i < SecondaryObjectives.Count; i++)
        {
            if (SecondaryObjectives[i].Type == ObjectiveType.Fuel)
            {
                SecondaryObjectives[i].CurrentCount = SecondaryObjectives[i].requiredCount * fraction;
                if (SecondaryObjectives[i].CheckComplete())
                {
                    Destroy(SecondaryObjectives[i]);
                    SecondaryObjectives.RemoveAt(i);

                }
            }

        }

    }
    void UpdateandCheckEnemies()
    {
        for (int i = 0; i < MainObjectives.Count; i++)
        {
            if (MainObjectives[i].Type == ObjectiveType.Enemy)
            {
                MainObjectives[i].CurrentCount += 1;
                if (MainObjectives[i].CheckComplete())
                {
                    Destroy(MainObjectives[i]);
                    MainObjectives.RemoveAt(i);

                }
            }

        }
        CheckLevelDone();
        for (int i = 0; i < SecondaryObjectives.Count; i++)
        {
            if (SecondaryObjectives[i].Type == ObjectiveType.Enemy)
            {
            SecondaryObjectives[i].CurrentCount += 1;
                if (SecondaryObjectives[i].CheckComplete())
                {
                    Destroy(SecondaryObjectives[i]);
                    SecondaryObjectives.RemoveAt(i);

                }
            }

        }
    }
    

    bool CheckLevelDone()
    {
        //Note that non active missions return false;
        for(int i=0;i < MainObjectives.Count; i++)
        {
            if (!MainObjectives[i].CheckComplete()) { return false; }
            Destroy(MainObjectives[i]);
            MainObjectives.RemoveAt(i);
        }
        return true;
    }
}