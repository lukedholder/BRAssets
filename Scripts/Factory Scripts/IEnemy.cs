using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    //enemy template variables
    int ID { get; set; }
    string type { get; set; }
    float health { get; set; }
    int baseDMG { get; set; }
    int pointValue { get; set; }

    //enemy template operations
    void Start();
    void Main();
    void Attack();
    void Move(GameObject gameObject, string direction);
    void Die();
}