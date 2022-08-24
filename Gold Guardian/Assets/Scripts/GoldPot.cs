using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldPot : MonoBehaviour
{
    public static GoldPot instance;

    [SerializeField] private float startingHealth;
    [SerializeField] private Slider healthbar;
    public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = startingHealth;
        instance = this;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);    // Declines the health but not below 0
        if (currentHealth > 0)
        {
            Debug.Log("ouch " + currentHealth);
            healthbar.value -= (_damage / 100);
        }
        else
        {
            Debug.Log("game over...");
            healthbar.value = 0;
            gameObject.SetActive(false);
        }
    }
}
