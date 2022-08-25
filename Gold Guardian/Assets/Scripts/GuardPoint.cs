using System.Collections.Generic;
using UnityEngine;

public class GuardPoint : MonoBehaviour {
    public Transform closestEnemy;
    public List<Transform> nearbyEnemies = new List<Transform>();

    void OnTriggerEnter(Collider other) {
        UnitType possibleEnemy = other.GetComponent<UnitType>();
        if (possibleEnemy && possibleEnemy.type == UnitType.Type.enemy) {
            nearbyEnemies.Add(possibleEnemy.transform);
        }
    }

    void OnTriggerExit(Collider other) {
        UnitType possibleEnemy = other.GetComponent<UnitType>();
        if (possibleEnemy && possibleEnemy.type == UnitType.Type.enemy) {
            nearbyEnemies.Remove(possibleEnemy.transform);
            if (possibleEnemy.transform == closestEnemy) {
                UpdateClosestEnemy();
            }
        }
    }

    public void UpdateClosestEnemy() {
        closestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        for (int i = nearbyEnemies.Count - 1; i >= 0; i--) {
            if (nearbyEnemies[i] != null) {
                float pathDistance = Vector3.Distance(transform.position, nearbyEnemies[i].position);
                if (pathDistance < shortestDistance) {
                    closestEnemy = nearbyEnemies[i];
                    shortestDistance = pathDistance;
                }
            } else {
                nearbyEnemies.RemoveAt(i);
            }
        }
    }
}