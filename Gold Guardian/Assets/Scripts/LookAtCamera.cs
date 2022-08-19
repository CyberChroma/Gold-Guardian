using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    void LateUpdate() {
        // Makes this gameobject look at the camera
        transform.LookAt(Camera.main.transform);
    }
}