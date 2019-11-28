using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSDead : MonoBehaviour
{
    public CharacterController FPSController;
    public static bool IsDead = false;
    public bool TestDead = false;
    Vector3 PhysicsVelocity;

    void Update()
    {
        IsDead = TestDead;

        if (IsDead)
        {        
            transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
            PhysicsVelocity.y = 0f;
            FPSController.Move(PhysicsVelocity * Time.deltaTime);
        }
    }
}
