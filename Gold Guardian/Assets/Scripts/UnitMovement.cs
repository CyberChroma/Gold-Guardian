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
        playerParent = GameObject.Find("Player Creatures").transform;
        enemyParent = GameObject.Find("Enemy Creatures").transform;
    }

    // Update is called once per frame
    void Update() {
        if (unitType.type == UnitType.Type.enemy) {
            // Go to the pot
            navMeshAgent.destination = goldPot.position;
        } else {
            if (!closestOpposingCreature) {
                GetClosestEnemy();
            }
            // Go towards the closest enemy
            navMeshAgent.destination = closestOpposingCreature.position;
        }
    }

    void GetClosestEnemy() {
        // Temp get list of all enemies from some enemy spawner
        NavMeshAgent[] allEnemies = enemyParent.GetComponentsInChildren<NavMeshAgent>();
        closestOpposingCreature = allEnemies[0].transform;
    }
}