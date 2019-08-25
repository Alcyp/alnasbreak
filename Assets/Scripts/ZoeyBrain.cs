using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoeyBrain : MonoBehaviour
{
    Animator animatorLight;
    public Animator animatorDark;
    CharacterController2D controller;
    float velocity = 0f;
    float moveTimer = 0f;
    public bool attack = false;

    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        animatorLight = GetComponent<Animator>();
    }

    void Update()
    {
        if (moveTimer > 0f) {
            moveTimer -= Time.deltaTime;
        }
        else
        {
            velocity = 0f;
            if (attack)
            {
                attack = false;
                animatorLight.SetTrigger("Attack");
                animatorDark.SetTrigger("Attack");
            }
        }
        animatorLight.SetFloat("Speed", Mathf.Abs(velocity));
        animatorDark.SetFloat("Speed", Mathf.Abs(velocity));
    }

    void FixedUpdate()
    {
        controller.Move(velocity * Time.fixedDeltaTime, false, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveTimer = 1f;
            attack = true;
            float direction = collision.gameObject.transform.position.x - gameObject.transform.position.x;
            velocity = Mathf.Sign(direction) * 5f;
        }
    }
}
