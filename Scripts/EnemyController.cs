using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Profiling;

public class EnemyController : MonoBehaviour
{
    public IEnemy enemyBehaviourObject;
    private EnemyFactory enemyFactory = new EnemyFactory();
    public int ID;
    public int activeEnemyID;

    public Sprite thinSprite;

    public GameObject batman;
    private float lastPosition;
    private Transform sprite;




    //private static readonly ProfilerMarker myMarker = new ProfilerMarker("enemyMove");

    void Start()
    {
        //create new object of class {ID}
        enemyBehaviourObject = enemyFactory.GetEnemy(ID);
        //tracks the batman gameobject and moves toward it
        enemyBehaviourObject.Start();
        batman = GameObject.Find("Batman");
        

        //set sprite and other bahaviors: 
        sprite = transform.GetChild(0);
        SpriteRenderer spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        switch (ID) {
            case 0:
                spriteRenderer.sprite = thinSprite;
                break;
            //case 1:
            //    Debug.Log(ID);
            //case 2:
            //    Debug.Log(ID);
            default:
                Debug.Log("Sprite Error: no ID available");
                break;
        }
        
    }

    
    void Update()
    {
        
        enemyBehaviourObject.Main();


        //Logger.marker.Begin();
        //Debug.Log("Mobving");
        //moves the enemy gameObject toward batman until
        //it's a certain distance that way it's not taking 
        //up the same x,y location
        if (batman.transform.position.x - 2 > gameObject.transform.position.x) {
            enemyBehaviourObject.Move(gameObject, "right");
        } else if (batman.transform.position.x + 2 < gameObject.transform.position.x) {
            enemyBehaviourObject.Move(gameObject, "left");
        }
        if (batman.transform.position.y - 1 > gameObject.transform.position.y) {
            enemyBehaviourObject.Move(gameObject, "down");
        } else if (batman.transform.position.y + 1 < gameObject.transform.position.y) {
            enemyBehaviourObject.Move(gameObject, "up");
        }

        if (gameObject.transform.position.x < batman.transform.position.x) {
            sprite.gameObject.transform.eulerAngles = new Vector3(sprite.gameObject.transform.eulerAngles.x, 0, sprite.gameObject.transform.eulerAngles.z);

        } else if (gameObject.transform.position.x > batman.transform.position.x) {
            sprite.gameObject.transform.eulerAngles = new Vector3(sprite.gameObject.transform.eulerAngles.x, 180, sprite.gameObject.transform.eulerAngles.z);

        }

        lastPosition = gameObject.transform.position.x;
        //Logger.marker.End();
    }
}