﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPowers : MonoBehaviour
{
    SenseLogic sense;
    public bool inLight;
    LightController ray;
    Rigidbody2D rb;
    public float lightForce = 20f;
    public SpriteRenderer playerLight;
    public SpriteRenderer playerDark;
    public SpriteRenderer playerGlow;
    public GameObject playerBall;
    public GameObject lightRay;
    public BoxCollider2D boxCollider;

    private void Awake()
    {
        sense = GetComponentInChildren<SenseLogic>();
        ray = GetComponentInChildren<LightController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb.velocity.sqrMagnitude < 0.1f && playerBall.activeSelf) { TurnIntoHuman(); }
        if (playerBall.activeSelf) { return; }
        inLight = sense.inLight;
        RayLogic();
        
    }

    void RayLogic()
    {

        if (!inLight) { ray.turnedOn = false; return; }

        if (Input.GetAxis("Submit") == 0f) { ray.turnedOn = false; return; }
        


        ray.turnedOn = true;
        ray.rotation = AngleTowardsMouse();

        if (Input.GetMouseButtonDown(0))
        {
            TurnIntoBall(); //Change shape into light
            
            rb.AddForce(VectorTowardsMouse() * lightForce);
        }
    }

    void TurnIntoBall()
    {
        playerBall.SetActive(true);
        playerLight.enabled = false;
        playerDark.enabled = false;
        playerGlow.enabled = false;
        lightRay.SetActive(false);
        boxCollider.enabled = false;
        rb.gravityScale = 0.01f;
    }

    void TurnIntoHuman()
    {
        playerBall.SetActive(false);
        playerLight.enabled = true;
        playerDark.enabled = true;
        playerGlow.enabled = true;
        lightRay.SetActive(true);
        boxCollider.enabled = true;
        rb.gravityScale = 3f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("Next stage!");
            StartCoroutine(ChangeLevel());

        }
    }

    IEnumerator ChangeLevel()
    {
        float fadeTime = GameObject.Find("Main Camera").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    float AngleTowardsMouse()
    {
        Vector2 mousePos = VectorTowardsMouse();
        return Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg + 90f;
    }

    Vector2 VectorTowardsMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = mousePos - objectPos;
        return new Vector2(direction.x, direction.y);
    }
}
