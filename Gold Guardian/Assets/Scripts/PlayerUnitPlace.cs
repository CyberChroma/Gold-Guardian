using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitPlace : MonoBehaviour
{
    public GameObject testUnitPrefab;
    public GameObject testUnitPreviewPrefab;
    public Material previewMat;
    public Material previewCantPlaceMat;
    public Transform unitsParent;

    private bool canStartPlace;
    private bool inPlaceMode;
    private PlayerMove playerMove;
    private CameraFollow cameraFollow;
    private CameraZoom cameraZoom;
    private GameObject previewUnit;

    // Start is called before the first frame update
    void Start()
    {
        canStartPlace = false;
        inPlaceMode = false;
        playerMove = GetComponent<PlayerMove>();
        cameraFollow = FindObjectOfType<CameraFollow>();
        cameraZoom = cameraFollow.GetComponentInChildren<CameraZoom>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canStartPlace && Input.GetKeyDown(KeyCode.Space)) {
            canStartPlace = false;
            EnterPlaceMode();
        }
        else if (inPlaceMode) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                PlaceUnit();
            }
            else if (Input.GetKeyDown(KeyCode.Escape)) {
                Destroy(previewUnit);
                previewUnit = null;
                ExitPlaceMode();
            }
            if (previewUnit) {
                // Move preview cube
                //previewUnit.transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!canStartPlace && other.CompareTag("Gold Pot") && !inPlaceMode) {
            canStartPlace = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gold Pot")) {
            canStartPlace = false;
        }
    }

    void EnterPlaceMode()
    {
        inPlaceMode = true;
        playerMove.inPlaceMode = true;
        cameraFollow.inPlaceMode = true;
        cameraZoom.inPlaceMode = true;
        previewUnit = Instantiate(testUnitPreviewPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity, transform);
    }

    void ExitPlaceMode()
    {
        Destroy(previewUnit);
        previewUnit = null;
        inPlaceMode = false;
        playerMove.inPlaceMode = false;
        cameraFollow.inPlaceMode = false;
        cameraZoom.inPlaceMode = false;
    }

    void PlaceUnit()
    {
        GameObject newUnit = Instantiate(testUnitPrefab, previewUnit.transform.position, previewUnit.transform.rotation, unitsParent);
        previewUnit.transform.parent = unitsParent;
        ExitPlaceMode();
    }
}
