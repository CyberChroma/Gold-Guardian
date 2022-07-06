using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public Transform cameraPivot;

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
        moveDir = moveDir.z * cameraPivot.forward + moveDir.x * cameraPivot.right;

        rb.AddForce(moveDir * speed, ForceMode.Impulse);
    }
}
