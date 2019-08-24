using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float moveSpeed = 10f;
    float velocity = 0f;
    bool jump = false;

    void Update()
    {
        velocity = Input.GetAxisRaw("Horizontal") * moveSpeed;
        if (Input.GetButtonDown("Jump")) { jump = true; }

    }
    
    void FixedUpdate()
    {
        controller.Move(velocity * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    
}
