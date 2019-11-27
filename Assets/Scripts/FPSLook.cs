using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLook : MonoBehaviour
{

    public float InputSensitivity = 100f;
    public Transform FPSCamera;
    float FPSRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float LookX = Input.GetAxis("Mouse X") * InputSensitivity * Time.deltaTime;
        float LookY = Input.GetAxis("Mouse Y") * InputSensitivity * Time.deltaTime;
        FPSCamera.Rotate(Vector3.up * LookX);
        FPSRotation -= LookY;
        FPSRotation = Mathf.Clamp(FPSRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(FPSRotation, 0f, 0f);
    }
}
