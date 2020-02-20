using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMove : MonoBehaviour
{
    public CharacterController FPSController;
    public float PlayerSpeed = 12f;
    Vector3 PhysicsVelocity;
    public float WorldGravity = -9.81f;
    public float JumpHeight = 4f;
    public string MoveVerticalInput = "Vertical";
    public string MoveHorizontalInput = "Horizontal";
    public string JumpInput = "Jump";
    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;
    bool IsGrounded = true;
    bool IsJumping = false;
    bool IsDoubleJumping = false;
    public bool AllowDoubleJump = true;

    public string CrouchInput = "ButtonB";
    public float crouchingHeight = 1.9f;
    public CharacterController characterController;

    void Update()
    {
        if (!FPSDead.IsDead) {
            IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
            FPSController = GetComponent<CharacterController>();
            if (IsGrounded && PhysicsVelocity.y < 0)
            {
                FPSController.slopeLimit = 45.0f;
                PhysicsVelocity.y = -2f;
                Debug.Log(IsGrounded);
                IsDoubleJumping = false;
                IsJumping = false;
            }
            float MoveX = Input.GetAxisRaw(MoveVerticalInput);
            float MoveY = Input.GetAxisRaw(MoveHorizontalInput);
            Vector3 MoveFPS = Vector3.Normalize(transform.right * MoveX + transform.forward * MoveY);
            FPSController.Move(MoveFPS * PlayerSpeed * Time.deltaTime);

            /*  if (Input.GetButtonDown(CrouchInput) && IsGrounded)
              {
                  characterController.height = crouchingHeight;
                  Debug.Log("Crouched");
              } */
            if (IsJumping && !IsDoubleJumping && AllowDoubleJump && Input.GetButtonDown(JumpInput))
            {
                Debug.Log("Is Double Jumping");
                FPSController.slopeLimit = 100.0f;
                PhysicsVelocity.y = Mathf.Sqrt(JumpHeight * -4f * WorldGravity);
                IsDoubleJumping = true;
            } else if (!IsJumping && Input.GetButtonDown(JumpInput))
            {
                Debug.Log("Is Jumping");
                FPSController.slopeLimit = 100.0f;
                PhysicsVelocity.y = Mathf.Sqrt(JumpHeight * -2f * WorldGravity);
                IsDoubleJumping = false;
                IsJumping = true;
            }

            PhysicsVelocity.y += WorldGravity * Time.deltaTime;
            FPSController.Move(PhysicsVelocity * Time.deltaTime);
        }
    }
}
