using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HUDManager : MonoBehaviour {
    public LevelManager LevelManager;

    public TMP_Text healthText;
    public TMP_Text timeText;
    // public TMP_Text ammoText;
    // public TMP_Text scoreText;
    // public TMP_Text waveText;

    void Update () {
        UpdateTime ();
    }

    public void UpdateHealth (float health) {
        healthText.text = ((int) health).ToString ();
    }
    public void UpdateTime () {
        timeText.text = LevelManager.GetLevelTimeString ();
    }

}