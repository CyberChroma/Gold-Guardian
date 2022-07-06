using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRange : MonoBehaviour
{

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawner;
    [SerializeField] private float launchVelocity;
    [SerializeField] private float cooldown;

    private float lifetimeCoolDown = Mathf.Infinity;

    void Update()
    {
        if (lifetimeCoolDown > cooldown)
        {
            GameObject projectileLaunch = Instantiate(projectile, projectileSpawner.position, projectileSpawner.rotation);
            projectileLaunch.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(launchVelocity, 0, 0));
            
            lifetimeCoolDown = 0;
        }
        lifetimeCoolDown += Time.deltaTime;
    }
}
