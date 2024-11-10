using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //bool moving = true;
    public Transform cam;
    public GameManager gameManager;// = new GameManager();

    void Start()
    {
        if (gameManager == null) {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        
        //Debug.Log(gameManager.score);
        //Debug.Log("Level Started");
        //cam.transform.position = transform.position;
    }


    void Update()
    {

        if (Input.GetKey(KeyCode.D)) {
            //print("D");
            transform.Translate(10 * Time.deltaTime,0,0);
        } else if (Input.GetKey(KeyCode.A)) {
            //print("A");
            transform.Translate(-10 * Time.deltaTime,0,0);
        }

        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(0, 10 * Time.deltaTime,0);
        } else if (Input.GetKey(KeyCode.S)) {
            transform.Translate(0, -10 * Time.deltaTime,0);
        }

        if (gameManager.enemiesSpawnedIn == 0) {
            //print("moving");
            if (transform.position.x > cam.position.x)
                cam.position = new Vector3(transform.position.x, cam.position.y, cam.position.z);
        }
    }
}
