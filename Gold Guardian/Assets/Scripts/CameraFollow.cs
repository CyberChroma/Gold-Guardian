using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float moveSmoothing;
    public Transform target;

    [HideInInspector] public bool inPlaceMode;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        if (!inPlaceMode) {
            transform.position = Vector3.Lerp(transform.position, target.position, moveSmoothing * Time.deltaTime);
        }
        else {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y - 1, target.position.z), moveSmoothing * Time.deltaTime);
        }
    }
}
