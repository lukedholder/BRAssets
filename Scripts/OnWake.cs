using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWake : MonoBehaviour
{
    private UserDB userDB = new UserDB();

    void Start()
    {
        Debug.Log("UserID on level wake: " + GameManager.currentUID);
        int[] userData = userDB.GetUserSaveData(GameManager.currentUID);
        for (int i = 0; i < userData.Length; i++) {
            //Debug.Log("userdata: " + userData[i]);
        }


        /*
        //if in GameManager, this gets called upon loading up the game
        //to the login screen which has no Uid(= -1)
        //user[] = {highscore, currscore, currlives, currhealth, testtubes, lastwave}
        if (gameManager.currentUID != -1) {
            int[] userData = userDB.GetUserSaveData(currentUID);
            Debug.Log("Score: " + userData[1]);
            //score = userData[1];
            //batman'sLives = userData[2];
            //batman'sHealth = userData[3];
            //batman'sTestTubes = userData[4];
            lastTriggerID = userData[5];
            //Batman.position = last trigger position
            //  if moving batman's position triggers the wave,
            //  then save its position, delete it first, then move him
            for (int i = 0; i <= lastTriggerID; i++) {
                string triggerName = $"EnemySpawnTrigger ({i})";
                GameObject trigger = GameObject.Find(triggerName);
                Destroy(trigger);
            }
        }
        */
    }
}
