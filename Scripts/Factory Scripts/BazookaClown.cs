using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaClown// : IEnemy
{
    public int ID { get; set; } = 1;
    public string type { get; set; } = "BAZOOKA CLOWN";
    public float health  { get; set; } = 30;
    public int baseDMG { get; set; } = 40;
    public int pointValue { get; set; } = 800;


    public GameManager gameManager;
    
    
    public void Main() { //called every frame in EnemyController's Update()
        //Debug.Log("bazooka is active");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Attack() {
        Debug.Log("bazooka attacked");
    }

    public void Move() {
        Debug.Log("bazooka moved");
    }


    public void Die() {
        //Debug.Log(gameManager);
        gameManager.enemiesSpawnedIn--;
        Debug.Log("bazooka died");
    }
}
