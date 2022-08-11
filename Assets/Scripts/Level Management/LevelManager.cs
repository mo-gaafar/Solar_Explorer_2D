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

    public void UpdateandCheckFuel(float value)
    {
        for (int i = 0; i < MainObjectives.Count; i++)
        {
            if (MainObjectives[i].Type == ObjectiveType.Fuel)
            {
                MainObjectives[i].CurrentCount += value;
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
                SecondaryObjectives[i].CurrentCount += value;
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

    public void UpdateandCheckStarshipKey(float value)
    {
        for (int i = 0; i < MainObjectives.Count; i++)
        {
            if (MainObjectives[i].Type == ObjectiveType.StarshipKey)
            {
                MainObjectives[i].CurrentCount += value;
                if (MainObjectives[i].CheckComplete())
                {
                    Destroy(MainObjectives[i]);
                    MainObjectives.RemoveAt(i);

                }
            }

        }
        CheckLevelDone();
    }

    public void  CheckLevelDone()
    {
        //Note that non active missions return false;
        /*Debug.Log("Checking Done")*/;
                //Debug.Log("Checking....");
        for(int i=0;i < MainObjectives.Count; i++)
        {
            //Debug.Log("Now Checking"+ MainObjectives[i].Type);
            if (MainObjectives[i].CheckComplete()) {
                //Debug.Log("Truly done in the name of god");
                Destroy(MainObjectives[i]);
                MainObjectives.RemoveAt(i);
            }
        }
        if (MainObjectives.Count == 0)
        {
            //LevelDone();
            LevelDone();
        }

    }

    void LevelDone()
    {
        
       SceneManager.LoadScene(1);
      
    }
}