using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Combat : MonoBehaviour
{
    private int baseAttack {get; set;} // used for the base attack damage
    private int stunValue {get; set;} // used for stun value for attacks
    private bool inRange {get; set;} // used for if batman is in range of the enemy
    private bool HoldingEnemy {get; set;} // used if batman is holding the enemy
    private Animator animator; // main animator class


    // main Attack class
    public void Attack()
    {
        Punch1();
        void Punch1()
        {
            animator.SetTrigger("Attack1");
            Debug.Log("Attack!");
        }

    }

    // Main Batarang class
    public void Batarang()
    {
        Debug.Log("Throw!");
        animator.SetTrigger("Throw");
    }

    // Main Test Tube class
    public void TestTube()
    {
        Debug.Log("Using Test Tube!");
        animator.SetTrigger("TestTube");
    }

    // Main Block class
    public void Block()
    {
        Debug.Log("Block!");
        animator.SetBool("IsBlocking", true);
    }
}
