using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float rotSpeed;
    public float groundCheckDistance;

    public float pushBackMaxSpeed;
    public float pushBackMaxDistance;

    public Transform cameraPivot;
    public Transform playerCamera;

    private Vector3 moveDir;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Turn();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W)) {
            moveDir.z = 1;
        }
        else if (Input.GetKey(KeyCode.S)) {
            moveDir.z = -1;
        }
        else {
            moveDir.z = 0;
        }

        if (Input.GetKey(KeyCode.D)) {
            moveDir.x = 1;
        }
        else if (Input.GetKey(KeyCode.A)) {
            moveDir.x = -1;
        }
        else {
            moveDir.x = 0;
        }

        if (Input.GetKey(KeyCode.E)) {
            moveDir.y = 1;
        }
        else if (Input.GetKey(KeyCode.Q)) {
            moveDir.y = -1;
        }
        else {
            moveDir.y = 0;
        }

        moveDir = moveDir.z * playerCamera.forward + moveDir.x * playerCamera.right + moveDir.y * playerCamera.up;

        rb.AddForce(moveDir * speed, ForceMode.Impulse);
    }

    void Turn()
    {
        //Vector3 axisTowardsDown = Vector3.Cross(playerCamera.forward, Vector3.down);
        //Vector3 cameraAngledDown = Quaternion.AngleAxis(10, axisTowardsDown) * playerCamera.forward;

        Vector3 startForward = transform.forward;
        Vector3 targetForward = playerCamera.forward;
        Vector3 torqueForwardDir = Vector3.Cross(startForward, targetForward);
        rb.AddTorque(torqueForwardDir * rotSpeed);

        Vector3 startUp = transform.up;
        RaycastHit hit;
        Vector3 targetUp = Vector3.up;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance, 1 << 6)) {
            targetUp = hit.normal;
        }
        Vector3 torqueUpDir = Vector3.Cross(startUp, targetUp);
        rb.AddTorque(torqueUpDir * rotSpeed);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, pushBackMaxDistance, 1 << 6)) {
                PushBack(hit);
            }
            if (Physics.Raycast(transform.position, transform.forward, out hit, pushBackMaxDistance, 1 << 6)) {
                PushBack(hit);
            }
            if (Physics.Raycast(transform.position, -transform.forward, out hit, pushBackMaxDistance, 1 << 6)) {
                PushBack(hit);
            }
            if (Physics.Raycast(transform.position, transform.right, out hit, pushBackMaxDistance, 1 << 6)) {
                PushBack(hit);
            }
            if (Physics.Raycast(transform.position, -transform.right, out hit, pushBackMaxDistance, 1 << 6)) {
                PushBack(hit);
            }
            if (Physics.Raycast(transform.position, transform.up, out hit, pushBackMaxDistance, 1 << 6)) {
                PushBack(hit);
            }
        }
    }

    void PushBack(RaycastHit hit)
    {
        Vector3 closestGroundDir = transform.position - hit.point;
        float distancePercentage = 1 - Vector3.Distance(transform.position, hit.point) / pushBackMaxDistance;
        float pushBackSpeed = Mathf.Lerp(0, pushBackMaxSpeed, distancePercentage + 0.1f);
        rb.AddForce(closestGroundDir * pushBackSpeed, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, -transform.up * pushBackMaxDistance);
    }
}
