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
    int health = 10;
    public GameObject healthBar;
    public bool attacking;

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "LightBall")
        {
            if (!attacking)
            {
                if (attack) { return; }
                moveTimer = 1f;
                attack = true;
                float direction = collision.gameObject.transform.position.x - gameObject.transform.position.x;
                velocity = Mathf.Sign(direction) * 5f;
            }
            else
            {

                Debug.Log("Player hit!");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "LightBall")
        {
            if (health == 10) { healthBar.SetActive(true); }
            health--;
            healthBar.transform.localScale = new Vector3(0.2f * health, 0.2f, 1f);
            if (health == 0)
            {
                gameObject.SetActive(false);
            }
        }
        
    }
}
