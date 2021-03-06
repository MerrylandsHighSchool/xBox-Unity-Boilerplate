﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchController : MonoBehaviour
{
    public float speed = 50;
    public float crouchingHeight;
    public float standingHeight;
    public float camStandingHeight;
    public float camCrouchingHeight;
    private bool isCrouchTransitionInProgress = false;
    public Transform characterControllerTransform;
    public CharacterController characterController;
    public Transform FPSCamera;
    public string CrouchInput = "Fire";

    private bool isCrouching = false;

    private void FixedUpdate()
    {
        if (Input.GetButtonDown(CrouchInput))
        {
            isCrouchTransitionInProgress = true;

            if (isCrouching)
            {
                isCrouching = false;
                characterController.height = standingHeight;
                characterController.center = new Vector3(0, 1.0f, 0);
                float newYPosition = Mathf.RoundToInt((float)characterControllerTransform.position.y + 0.1f);
                characterControllerTransform.position = new Vector3(characterControllerTransform.position.x, newYPosition, characterControllerTransform.position.z);
}
            else
            {
                isCrouching = true;
                characterController.height = crouchingHeight;
                characterController.center = new Vector3(0, 0.5f, 0);
            }
        }

        if (isCrouchTransitionInProgress)
        {
            Vector3 camPosition = FPSCamera.localPosition;
            Vector3 standCamPosition = new Vector3(0, camStandingHeight, 0);
            Vector3 crouchCamPosition = new Vector3(0, camCrouchingHeight, 0);

            if (isCrouching)
            {
                CamLerpToPosition(camPosition, crouchCamPosition);
            }
            else
            {
                CamLerpToPosition(camPosition, standCamPosition);
            }
        }
    }

    private void CamLerpToPosition(Vector3 currentPosition, Vector3 targetPosition)
    {
        FPSCamera.localPosition = Vector3.Lerp(currentPosition, targetPosition, Time.fixedDeltaTime * speed);

        if (Mathf.Abs(FPSCamera.localPosition.y - targetPosition.y) < 0.01f)
        {
            isCrouchTransitionInProgress = false;
            Debug.Log("Reached " + (isCrouching ? "crouching" : "standing") + " height");
        }
    }
}