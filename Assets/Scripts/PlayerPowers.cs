using System.Collections;
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
    public SpriteMask playerGlow;
    public GameObject playerBall;
    public GameObject lightRay;
    BoxCollider2D boxCollider;
    bool ballForm;
    CharacterController2D controller;
    float morphTimer = 0f;
    Vector2 speedBeforeMirror;
    bool fly = false;

    private void Awake()
    {
        sense = GetComponentInChildren<SenseLogic>();
        ray = GetComponentInChildren<LightController>();
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (ballForm) { inLight = false; }
        else
        {
            inLight = sense.inLight;
            RayLogic();
        }
        RotateLightBall();
    }

    void RotateLightBall()
    {
        Vector3 temp = playerBall.transform.eulerAngles;
        Vector2 normal = rb.velocity.normalized;
        temp.z = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg - 90f;
        playerBall.transform.eulerAngles = temp;
    }

    void RayLogic()
    {
        if (!inLight) { ray.turnedOn = false; return; } //in light?

        if (Input.GetAxis("Submit") == 0f && !Input.GetMouseButton(1)) { ray.turnedOn = false; return; } //casting?

        ray.turnedOn = true;
        ray.rotation = AngleTowardsMouse();

        if (Input.GetMouseButtonDown(0))
        {
            TurnIntoBall(); //Change shape into light
            fly = true;
        }
    }

    private void FixedUpdate()
    {
        if (fly)
        {
            morphTimer -= Time.deltaTime;
            if (morphTimer > 0f) { return; }
            rb.AddForce(VectorTowardsMouse() * lightForce);
            fly = false;
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
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        ballForm = true;
        controller.m_AirControl = false;
        morphTimer = 0.2666f;
    }

    void TurnIntoHuman()
    {
        StartCoroutine(ActivateAnimator());
    }

    IEnumerator ActivateAnimator()
    {
        playerBall.GetComponent<Animator>().SetTrigger("End");
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.2666f);
        playerBall.SetActive(false);
        playerLight.enabled = true;
        playerDark.enabled = true;
        playerGlow.enabled = true;
        lightRay.SetActive(true);
        boxCollider.enabled = true;
        rb.gravityScale = 3f;
        ballForm = false;
        controller.m_AirControl = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (ballForm) { TurnIntoHuman(); }
        }
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
        direction.Normalize();
        return new Vector2(direction.x, direction.y);
    }
}
