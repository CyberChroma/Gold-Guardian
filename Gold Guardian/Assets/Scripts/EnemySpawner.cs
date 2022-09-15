using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] float spawnRadius = 10;
    [SerializeField] private int spawnCount = 10;
    [SerializeField] private int timeBetweenSpawn;
    [SerializeField] private GameObject Enemy;

    void Awake() {
        StartCoroutine(WaveCooldown());
    }

    IEnumerator WaveCooldown() {
        for (int i = 0; i < spawnCount; i++) {
            Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;

            GameObject spawnEnemy = Instantiate(Enemy);
            spawnEnemy.transform.position = pos;
            spawnEnemy.transform.forward = transform.forward;

            yield return new WaitForSeconds(timeBetweenSpawn);
        }
        
    }
}
