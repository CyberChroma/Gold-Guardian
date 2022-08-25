using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour {
    public enum MovementType {
        guardArea,
        runForEnemies,
        followPlayer
    }
    public MovementType movementType;

    private Transform closestOpposingCreature;
    private Transform goldPot;

    private Transform playerParent;
    private Transform enemyParent; 
    private NavMeshAgent navMeshAgent;
    private UnitType unitType;

    // Start is called before the first frame update
    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        unitType = GetComponent<UnitType>();
        goldPot = GoldPot.instance.transform;
        playerParent = GameObject.Find("PlayerCreatures").transform;
        enemyParent = GameObject.Find("EnemyCreatures").transform;
    }

    // Update is called once per frame
    void Update() {
        if (unitType.type == UnitType.Type.enemy) {
            // Go to the pot
            navMeshAgent.destination = goldPot.position;
        } else {
            if (closestOpposingCreature == null) {
                GetClosestEnemy();
            }
            // Go towards the closest enemy
            navMeshAgent.destination = closestOpposingCreature.position;
        }
    }

    void GetClosestEnemy() {
        NavMeshAgent[] allEnemies = enemyParent.GetComponentsInChildren<NavMeshAgent>();
        float shortestDistance = Mathf.Infinity;
        NavMeshPath path = new NavMeshPath();
        for (int i = 0; i < allEnemies.Length; i++) {
            navMeshAgent.CalculatePath(allEnemies[i].transform.position, path);
            float pathDistance = 0;
            for (int j = 0; j < path.corners.Length - 1; j++) {
                pathDistance += Vector3.Distance(path.corners[j], path.corners[j + 1]);
            }
            if (pathDistance < shortestDistance) {
                closestOpposingCreature = allEnemies[i].transform;
                shortestDistance = pathDistance;
            }
        }
        navMeshAgent.destination = closestOpposingCreature.position;
    }
}