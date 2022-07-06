using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSmoothing;
    public float maxZoomDis;
    public float playerHeightOffset;
    public float zoomBuffer;
    public Transform camPivot;
    public LayerMask environmentLayer;

    private float zoomDistance;
    private Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        zoomDistance = 0;
        startingPos = transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Zoom();
    }

    void Zoom()
    {
        RaycastHit hit;
        if (Physics.Linecast(camPivot.position + Vector3.up * playerHeightOffset, transform.position, out hit, environmentLayer, QueryTriggerInteraction.Ignore)) {
            zoomDistance = Mathf.Lerp(zoomDistance, Vector3.Distance(camPivot.TransformPoint(startingPos), hit.point) + zoomBuffer, zoomSmoothing * Time.deltaTime);
            if (zoomDistance >= maxZoomDis) {
                zoomDistance = maxZoomDis;
            }
        }
        else if (zoomDistance != 0) {
            if (Physics.Linecast(transform.position, camPivot.TransformPoint(startingPos), out hit, environmentLayer, QueryTriggerInteraction.Ignore)) {
                zoomDistance = Mathf.Lerp(zoomDistance, Vector3.Distance(camPivot.TransformPoint(startingPos), hit.point) + zoomBuffer, zoomSmoothing * Time.deltaTime);
            }
            else {
                zoomDistance = Mathf.Lerp(zoomDistance, 0, zoomSmoothing * Time.deltaTime);
            }
        }
        transform.position = camPivot.TransformPoint(startingPos) + transform.forward * zoomDistance;
        transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
    }
}
