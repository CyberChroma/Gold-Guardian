using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float lifetime;
    [SerializeField] private float damage;

    void Update()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.tag == "Gold Pot")
        {
            Debug.Log("hit the prize!");
            collision.gameObject.GetComponent<GoldPot>().TakeDamage(damage);
        }
    }

}