using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour {
    public Transform target;

    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        navMeshAgent.destination = target.position;
    }
}