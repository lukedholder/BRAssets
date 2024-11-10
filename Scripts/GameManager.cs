using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Profiling;




public class GameManager : MonoBehaviour
{
    public static GameManager gameManager; // global singleton instance
    
    public static int currentUID = -1; // holds uid of user that is currently logged in
    //test variable to show current ID in Unity inspector
    public int inspectorID;
    


    

    public static int lastTriggerID;
    
    public GameObject enemyBlankPrefab;
    public static int score = 0;
    public int enemiesLeftToSpawn = 0;
    public int enemiesSpawnedIn = 0;

    public int[] enemyQueue = new int[5];
    public int enIdx = 0;
    public GameObject[] activeEnemies = new GameObject[5];

    public float spawnRate = 3;
    private float nextSpawnTime = 0f;


    //static string marker = "spawnEnemy";
    //private static readonly ProfilerMarker methodMarker = new ProfilerMarker("spawnEnemy");
    

    void Awake()
    {
        //temp assignment
        //currentUID = 4;
        //lastTriggerID = 2;
        //Debug.Log("double test");
        Debug.Log("Current UID: " + currentUID);
        if (gameManager != null && gameManager != this)
        {
            Destroy(gameObject);
        } else
        {
            gameManager = this;
        }
        
        

        // preserve the GameManager instance when new scenes are loaded
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    void Update()
    {
        //temp dev tool to kill enemy: die()
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            ForceKill(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha1)) {
            //int[] k = {0, -1, -1, -1, -1, 1, 0};
            //SpawnEnemies(k);
            ForceKill(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ForceKill(2);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ForceKill(3);
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            ForceKill(4);
        }

        inspectorID = currentUID;
        SpawnEnemy();
        
    }

    //temp dev tool:
    public void ForceKill(int activeEnIdx) {
        if (activeEnemies[activeEnIdx] != null) {
            
            GameObject enemy = activeEnemies[activeEnIdx];
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.enemyBehaviourObject.Die();
            Destroy(enemy);
        }
    }

    public void SpawnEnemy() {
        //methodMarker.Begin();
        //an enemy will only spawn if there is another one in queue,
        //less than three spawned in at a time, and if it's been
        //more than 5 seconds since the last
        if (enemiesLeftToSpawn > 0 && enemiesSpawnedIn < 3 && Time.time >= nextSpawnTime && enIdx < 5) {
            
            int ID = enemyQueue[enIdx];

            GameObject enemySpawnPoint = GameObject.Find("EnemySpawnPoint");

            GameObject enemyObject = Instantiate(enemyBlankPrefab, enemySpawnPoint.transform.position, Quaternion.identity);
            EnemyController enemyController = enemyObject.GetComponent<EnemyController>();
            enemyController.ID = ID;
            enemyController.activeEnemyID = enIdx;
            activeEnemies[enIdx] = enemyObject;

            Debug.Log("Spawned in enemyID: " + ID + "  activeID: " + enemyController.activeEnemyID);
            enemiesLeftToSpawn--;
            enemiesSpawnedIn++;
            enIdx++;
            nextSpawnTime = Time.time + spawnRate;
            
        //resets the enemy index after all enemies die
        } 
        if (enemiesLeftToSpawn == 0 && enemiesSpawnedIn == 0) {
            enIdx = 0;
            //AutoSave()
        }
        //methodMarker.End();
        
        
        
        
        
    }


    //Sets up the queue and metadata
    public void SpawnEnemies(int[] enemyArray) {
        //methodMarker.Begin();
        //enemy array is formated as shown:
        //{x,x,x,x,x,numOfEnemies, triggerID}
        enemiesLeftToSpawn = enemyArray[5];
        lastTriggerID = enemyArray[6];

        for (int i = 0; i < enemiesLeftToSpawn; i++) {
            enemyQueue[i] = enemyArray[i];
        }
        //methodMarker.End();
        //Logger.Log("spawnEnemies");
    }

    //autosave to be inplemented with integration
    public void AutoSave(int numOfEnemies, int triggerID) {

    }
}