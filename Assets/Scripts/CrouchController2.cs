using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchController2 : MonoBehaviour
{
    public float speed = 50;
    public float crouchingHeight;
    public float standingHeight;
    public float camStandingHeight;
    public float camCrouchingHeight;
    private bool isCrouchTransitionInProgress = false;
    public CharacterController characterController;
    public Transform characterControllerTransform;
    public Transform FPSCamera;
    public string CrouchInput = "ButtonB";

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
              //  characterControllerTransform.position = new Vector3(0, 5f, 0);
            }
        } else {
                isCrouching = true;
                characterController.height = crouchingHeight;
                characterController.center = new Vector3(0, 0.5f, 0);
        }

         if (isCrouchTransitionInProgress)
               {
                   Vector3 camPosition = FPSCamera.localPosition;
                   Vector3 standCamPosition = new Vector3(0f, camStandingHeight, 0f);
                   Vector3 crouchCamPosition = new Vector3(0f, camCrouchingHeight, 0f);

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

         if (Mathf.Abs(FPSCamera.position.y - targetPosition.y) < 0.01f)
         {
             isCrouchTransitionInProgress = false;
           //  Debug.Log("Reached " + (isCrouching ? "crouching" : "standing") + " height");
         } 
    }
}