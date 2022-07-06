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
            projectileLaunch.transform.position = projectileSpawner.position;

            Rigidbody body = projectileLaunch.GetComponent<Rigidbody>();
            body.AddRelativeForce(new Vector3(25, 0, 0), ForceMode.Impulse);
            
            lifetimeCoolDown = 0;
        }
        lifetimeCoolDown += Time.deltaTime;
    }
}
