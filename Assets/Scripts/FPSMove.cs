using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMove : MonoBehaviour
{
    public CharacterController FPSController;
    public float PlayerSpeed = 12f;
    Vector3 PhysicsVelocity;
    public float WorldGravity = -9.81f;
    public float JumpHeight = 3f;

    void Update()
    {
        FPSController = GetComponent<CharacterController>();
        if(FPSController.isGrounded && PhysicsVelocity.y < 0)
        {
            FPSController.slopeLimit = 45.0f;
            PhysicsVelocity.y = -2f;
        }
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");
        Vector3 MoveFPS = Vector3.Normalize(transform.right * MoveX + transform.forward * MoveY);
        FPSController.Move(MoveFPS * PlayerSpeed * Time.deltaTime);
        if(Input.GetButtonDown("Jump") && FPSController.isGrounded)
        {
            FPSController.slopeLimit = 100.0f;
            PhysicsVelocity.y = Mathf.Sqrt(JumpHeight * -2f * WorldGravity);
        }
        PhysicsVelocity.y += WorldGravity * Time.deltaTime;
        FPSController.Move(PhysicsVelocity * Time.deltaTime);
    }
}
