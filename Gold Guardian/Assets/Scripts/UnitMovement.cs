using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour {
    public enum MovementType {
        guardArea,
        runForEnemies,
        followPlayer
    }
    public MovementType movementType;

    public Transform target;

    private NavMeshAgent navMeshAgent;
    private UnitType unitType;

    // Start is called before the first frame update
    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        unitType = GetComponent<UnitType>();
    }

    // Update is called once per frame
    void Update() {
        if (unitType.type == UnitType.Type.enemy) {
            // Temp get pot
            navMeshAgent.destination = target.position;
        } else {
            // Temp get enemy unit
            navMeshAgent.destination = target.position;
        }
    }
}