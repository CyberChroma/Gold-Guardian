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

    [HideInInspector] public bool inPlaceMode;

    private float zoomDistance;
    private Vector3 startingPos;
    private Vector3 startingPlacePos;
    private float startingRot;
    private float startingPlaceRot;

    // Start is called before the first frame update
    void Start()
    {
        zoomDistance = 0;
        startingPos = transform.localPosition;
        startingPlacePos = new Vector3(0, 2.25f, -2.5f);
        startingRot = transform.localRotation.eulerAngles.x;
        startingPlaceRot = 40;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Zoom();
    }

    void Zoom()
    {
        if (!inPlaceMode) {
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
            transform.position = Vector3.Lerp(transform.position, camPivot.TransformPoint(startingPos) + transform.forward * zoomDistance, zoomSmoothing / 2 * Time.deltaTime);
            transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(startingRot, 0, 0), zoomSmoothing / 2 * Time.deltaTime);
        }
        else {
            RaycastHit hit;
            if (Physics.Linecast(camPivot.position + Vector3.up * playerHeightOffset, transform.position, out hit, environmentLayer, QueryTriggerInteraction.Ignore)) {
                zoomDistance = Mathf.Lerp(zoomDistance, Vector3.Distance(camPivot.TransformPoint(startingPlacePos), hit.point) + zoomBuffer, zoomSmoothing * Time.deltaTime);
                if (zoomDistance >= maxZoomDis) {
                    zoomDistance = maxZoomDis;
                }
            }
            else if (zoomDistance != 0) {
                if (Physics.Linecast(transform.position, camPivot.TransformPoint(startingPlacePos), out hit, environmentLayer, QueryTriggerInteraction.Ignore)) {
                    zoomDistance = Mathf.Lerp(zoomDistance, Vector3.Distance(camPivot.TransformPoint(startingPlacePos), hit.point) + zoomBuffer, zoomSmoothing * Time.deltaTime);
                }
                else {
                    zoomDistance = Mathf.Lerp(zoomDistance, 0, zoomSmoothing * Time.deltaTime);
                }
            }
            transform.position = Vector3.Lerp(transform.position, camPivot.TransformPoint(startingPlacePos) + transform.forward * zoomDistance, zoomSmoothing / 5 * Time.deltaTime);
            transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(startingPlaceRot, 0, 0), zoomSmoothing / 5 * Time.deltaTime);
        }
    }
}
