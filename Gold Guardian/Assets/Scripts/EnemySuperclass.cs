using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuperclass : MonoBehaviour
{
    // Placeholder values
    public int health;
    public float speed;
    public string speedBoostType;
    public int damage;
    public int resistance;
    
    public void EnemySuperClass()
    {
        health = 200;
        speed = 0f;
        speedBoostType = "ice";
        damage = 10;
        resistance = 5;
    }

    public void Update()
    {
        
    }

    public void Attack()
    {

    }
    
    public void TakeDamage()
    {

    }
}
