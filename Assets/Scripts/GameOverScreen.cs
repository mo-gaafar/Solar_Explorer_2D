using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour {

    public TMPro.TextMeshProUGUI coinText;

    public void ShowGameOverScreen (float coinAmount) {
        gameObject.SetActive (true);
        coinText.text = coinAmount.ToString ();
    }
}