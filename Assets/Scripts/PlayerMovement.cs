using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public GameObject playerBall;
    public Animator animatorLight;
    public Animator animatorDark;

    public float moveSpeed = 10f;
    float velocity = 0f;
    bool jump = false;

    void Update()
    {
        velocity = Input.GetAxisRaw("Horizontal") * moveSpeed;

        animatorLight.SetFloat("Speed", Mathf.Abs(velocity));
        animatorDark.SetFloat("Speed", Mathf.Abs(velocity));

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

        if (controller.m_Grounded == false)
        {
            animatorLight.SetBool("isJumping", true);
            animatorDark.SetBool("isJumping", true);
        }
    }
    
    void FixedUpdate()
    {
        if (playerBall.activeSelf) { jump = false; return; }
        controller.Move(velocity * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void OnLanding()
    {
        animatorLight.SetBool("isJumping", false);
        animatorDark.SetBool("isJumping", false);
    }
}
