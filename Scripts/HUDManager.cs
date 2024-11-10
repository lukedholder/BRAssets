using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    //private GameManager gameManager;
    public TMP_Text scoreTXT;
    public TMP_Text livesLeftTXT;
    public TMP_Text testTubesLeftTXT;
    public TMP_Text  currentEnemyTXT;

    void Start()
    {
        // if (gameManager == null) {
        //     gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // }
    }

    void Update()
    {
        //Score text:
        if (GameManager.score != null) {
            scoreTXT.SetText("SCORE  " + GameManager.score);
        } 
    }
}
