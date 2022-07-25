using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPot : MonoBehaviour
{

    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);    // Declines the health but not below 0
        if (currentHealth > 0)
        {
            // display lower
        }
        else
        {
            // game over
        }
    }
}
