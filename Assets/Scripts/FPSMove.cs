using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMove : MonoBehaviour
{
    public CharacterController FPSController;
    public float PlayerSpeed = 12f;
    Vector3 PhysicsVelocity;
    public float WorldGravity = -9.81f;
    public float JumpHeight = 5f;
    public float DoubleJumpHeight = 2.5f;
    public string MoveVerticalInput = "Vertical";
    public string MoveHorizontalInput = "Horizontal";
    public string JumpInput = "Jump";
    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;
    bool IsGrounded;
    bool IsDoubleJumping = false;
    public bool AllowDoubleJump = true;

    void Update()
    {
        if (!FPSDead.IsDead) {
            IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
            FPSController = GetComponent<CharacterController>();
            if (IsGrounded && PhysicsVelocity.y < 0)
            {
                FPSController.slopeLimit = 45.0f;
                PhysicsVelocity.y = -2f;
            }
            float MoveX = Input.GetAxisRaw(MoveVerticalInput);
            float MoveY = Input.GetAxisRaw(MoveHorizontalInput);
            Vector3 MoveFPS = Vector3.Normalize(transform.right * MoveX + transform.forward * MoveY);
            FPSController.Move(MoveFPS * PlayerSpeed * Time.deltaTime);


            if (!IsDoubleJumping && !IsGrounded && AllowDoubleJump && Input.GetButtonDown(JumpInput))
            {
                Debug.Log("Double Jumping");
                FPSController.slopeLimit = 100.0f;
                PhysicsVelocity.y = Mathf.Sqrt(DoubleJumpHeight * -2f * WorldGravity);
                IsDoubleJumping = true;
            }
            if (Input.GetButtonDown(JumpInput) && IsGrounded)
            {
                Debug.Log("Jumping");
                FPSController.slopeLimit = 100.0f;
                PhysicsVelocity.y = Mathf.Sqrt(JumpHeight * -2f * WorldGravity);
                IsDoubleJumping = false;
            }
            PhysicsVelocity.y += WorldGravity * Time.deltaTime;
            FPSController.Move(PhysicsVelocity * Time.deltaTime);
        }
    }
}
