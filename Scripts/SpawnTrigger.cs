using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public int triggerID;
    public GameManager gameManager;

    //thin clown id = 0
    //bazooka clown id = 1
    //boss id = 2

    //assign enemy types in the Unity editor
    public string enemy1;
    public string enemy2;
    public string enemy3;
    public string enemy4;
    public string enemy5;

    private int[] enemyArray = new int[7];
    private int numOfEnemies = 0;


    void Start()
    {
        //automatically assign the GameManger to avoid having to do it manually and
        //avoid bugs during integrating
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        string[] enemyTypeArray = {enemy1, enemy2, enemy3, enemy4, enemy5};
        
        

        //for loop converts list of strings: enemy types, into list of int: IDs
        for (int i = 0; i < 5; i++) {
            if (enemyTypeArray[i] == "thin") {
                enemyArray[i] = 0;
                numOfEnemies++;
            } else if (enemyTypeArray[i] == "bazooka") {
                enemyArray[i] = 1;
                numOfEnemies++;
            } else if (enemyTypeArray[i] == "boss") {
                enemyArray[i] = 2;
                numOfEnemies++;
            } else {
                enemyArray[i] = -1; //empty indexes are assigned as -1 because null can't convert to it
            }

        }
        
        //adds metadata to their spots at the end of array that gets sent to GameManger.SpawnEnemies()
        enemyArray[5] = numOfEnemies;
        enemyArray[6] = triggerID;

        
    }

    public void remove() {
        Destroy(gameObject);
    }

    //Obsever Pattern:
    //the trigger (subject) will notify the gameManager (observer)
    //after its state has changed on player collision
    private void OnTriggerEnter2D(Collider2D other) {
        Logger.marker.Begin();
        if (other.CompareTag("Player")) {
            
            gameManager.SpawnEnemies(enemyArray);
            GameManager.lastTriggerID = triggerID;
            remove();
        }
        Logger.marker.End();
    }
}
