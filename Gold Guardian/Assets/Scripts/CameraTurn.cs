using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTurn : MonoBehaviour
{
    public float turnHorizontalSpeed = 4;
    public float turnVerticalSpeed = 2;
    public float minXTurn = -85;
    public float maxXTurn = 45;

    public bool HorizontalInvert;
    public bool VerticalInvert;

    private float mouseX;
    private float mouseY;
    private float XChange;
    private float YChange;
    private float newXRot;
    private float newYRot;

    // Update is called once per frame
    void Update()
    {
        Turn();
    }

    void Turn()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        XChange = -mouseY * turnVerticalSpeed * Time.timeScale;
        if (VerticalInvert) {
            XChange = -XChange;
        }
        newXRot = transform.rotation.eulerAngles.x + XChange;
        if (newXRot > 180) {
            newXRot = Mathf.Clamp(newXRot, minXTurn + 360, 370);
        } else {
            newXRot = Mathf.Clamp(newXRot, -10, maxXTurn);
        }

        YChange = mouseX * turnHorizontalSpeed * Time.timeScale;
        if (HorizontalInvert) {
            YChange = -YChange;
        }
        newYRot = transform.rotation.eulerAngles.y + YChange;

        Vector3 newRotation = new Vector3(newXRot, newYRot, 0);
        transform.rotation = Quaternion.Euler(newRotation);
    }
}
