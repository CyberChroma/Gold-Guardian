using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour {
    public enum MovementType {
        enemy,
        runForEnemies,
        guardArea,
        guardPot,
        followPlayer
    }
    public MovementType movementType;
    public GameObject guardPointPrefab;

    private NavMeshAgent navMeshAgent;
    private UnitType unitType;
    private Transform player;
    private Transform enemyParent;
    private Transform targetedOpposingCreature;
    private GuardPoint goldPotGuardPoint;
    private Transform guardPointsParent;
    private GuardPoint guardPoint;

    // Start is called before the first frame update
    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        unitType = GetComponent<UnitType>();
        goldPotGuardPoint = GoldPot.instance.GetComponentInChildren<GuardPoint>();
        enemyParent = GameObject.Find("EnemyCreatures").transform;
        player = GameObject.Find("Player").transform;
        guardPointsParent = GameObject.Find("GuardPoints").transform;
    }

    // Update is called once per frame
    void Update() {
        if (unitType.type == UnitType.Type.enemy) {
            // Go to the pot
            navMeshAgent.destination = goldPotGuardPoint.transform.position;
            navMeshAgent.stoppingDistance = 1.1f;
        } else {
            switch (movementType) {
                case MovementType.runForEnemies:
                    if (targetedOpposingCreature == null) {
                        GetClosestEnemy();
                    }
                    // Go towards the closest enemy
                    navMeshAgent.destination = targetedOpposingCreature.position;
                    navMeshAgent.stoppingDistance = 1.1f;
                    break;
                case MovementType.guardArea:
                    if (guardPoint == null) {
                        guardPoint = Instantiate(guardPointPrefab, transform.position, Quaternion.identity, guardPointsParent).GetComponent<GuardPoint>();
                        guardPoint.gameObject.name = transform.name + " GuardPoint";
                    }
                    if (guardPoint.closestEnemy == null) {
                        guardPoint.UpdateClosestEnemy();
                        // If there is no closest enemy
                        if (guardPoint.closestEnemy == null) {
                            navMeshAgent.destination = guardPoint.transform.position;
                            navMeshAgent.stoppingDistance = 0.1f;
                        } else {
                            navMeshAgent.destination = guardPoint.closestEnemy.position;
                            navMeshAgent.stoppingDistance = 1.1f;
                        }
                    } else {
                        navMeshAgent.destination = guardPoint.closestEnemy.position;
                        navMeshAgent.stoppingDistance = 1.1f;
                    }
                    break;
                case MovementType.guardPot:
                    if (goldPotGuardPoint.closestEnemy == null) {
                        goldPotGuardPoint.UpdateClosestEnemy();
                        // If there is no closest enemy
                        if (goldPotGuardPoint.closestEnemy == null) {
                            navMeshAgent.destination = goldPotGuardPoint.transform.position;
                            navMeshAgent.stoppingDistance = 3;
                        } else {
                            navMeshAgent.destination = goldPotGuardPoint.closestEnemy.position;
                            navMeshAgent.stoppingDistance = 1.1f;
                        }
                    } else {
                        navMeshAgent.destination = goldPotGuardPoint.closestEnemy.position;
                        navMeshAgent.stoppingDistance = 1.1f;
                    }
                    break;
                case MovementType.followPlayer:
                    navMeshAgent.destination = player.position;
                    navMeshAgent.stoppingDistance = 2f;
                    break;
            }
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
                targetedOpposingCreature = allEnemies[i].transform;
                shortestDistance = pathDistance;
            }
        }
        navMeshAgent.destination = targetedOpposingCreature.position;
    }
}