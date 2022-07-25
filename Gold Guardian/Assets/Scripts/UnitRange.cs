using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRange : MonoBehaviour
{

    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform projectileSpawner;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float cooldown;

    private float lifetimeCoolDown = Mathf.Infinity;
    private BoxCollider unitCollider;

    private void Start()
    {
        unitCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (lifetimeCoolDown > cooldown)
        {
            Projectile projectileLaunch = Instantiate(projectile, projectileSpawner.position, projectileSpawner.rotation) as Projectile;
            projectileLaunch.transform.position = projectileSpawner.position;
            Physics.IgnoreCollision(projectileLaunch.GetComponent<BoxCollider>(), unitCollider);


            Rigidbody body = projectileLaunch.GetComponent<Rigidbody>();
            body.AddRelativeForce(new Vector3(projectileSpeed, 0, 0), ForceMode.Impulse);

            lifetimeCoolDown = 0;
        }
        lifetimeCoolDown += Time.deltaTime;
    }
}