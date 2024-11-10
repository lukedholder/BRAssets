using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinClown : IEnemy
{
    //implemented Ienemy variables specific to the THIN CLOWN
    public int ID { get; set; } = 0;
    public string type { get; set; } = "THIN CLOWN";
    public float health  { get; set; } = 40;
    public int baseDMG { get; set; } = 15;
    public int pointValue { get; set; } = 500;
    public GameManager gameManager;
    public float moveSpd = 5;
    public GameObject batman;
    
    public void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Main() { //called in Update

    }

    //the attack method will be implemented during integration
    public void Attack() {
        Debug.Log("thin attacked");
    }
    //the move method gets called in the character controller
    public void Move(GameObject gameObject, string direction) {
        
        if (direction == "left") {
            gameObject.transform.Translate(-moveSpd * Time.deltaTime, 0,0);
        } else if (direction == "right" ) {
            gameObject.transform.Translate(moveSpd * Time.deltaTime, 0,0);
        }
        if (direction == "up") {
            gameObject.transform.Translate(0, -moveSpd * Time.deltaTime,0);
        } else if (direction == "down" ) {
            gameObject.transform.Translate(0, moveSpd * Time.deltaTime, 0);
        }
    }

    public void Die() {
        gameManager.enemiesSpawnedIn--;
        //score can be directly accessed from GameManager becuause its static
        GameManager.score += pointValue;
        Debug.Log("thin died");
    }

    public void Block() {

    }
}
