using UnityEngine;


public class CharacterCombat : MonoBehaviour
{
    public int health = 100;
    public int newHealth;
    public int lives = 3;

    // where i call the facade class
    public Combat combatSystem; 

    void Start()
    {
        Combat combatSystem = GetComponent<Combat>(); // get component of the facade class
    }

    // where the main funcitonality of the batman controller
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            combatSystem.Attack(); // calls the attack class
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            combatSystem.Batarang(); // calls the batarang class
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            combatSystem.TestTube(); // calls the test tube class
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            combatSystem.Block(); // calls the block class
        }
    }
    public void TakeDamage(int damage)
    {
        newHealth = health - damage;
        if (health < 0)
        {
            lives--;
            if(lives == 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        // display game over screen
    }
}

