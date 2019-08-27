using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoeyBrain : MonoBehaviour
{
    Animator animatorLight;
    public Animator animatorDark;
    CharacterController2D controller;
    float velocity = 0f;
    float moveTimer = 0f;
    public bool attack = false;
    int health = 5;
    public GameObject healthBar;
    public bool attacking;
    bool playerHit = false;

    public AudioSource swing;
    public AudioSource step;
    public AudioSource lightHit;
    public AudioSource axeHit;

    bool dead;

    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        animatorLight = GetComponent<Animator>();
    }

    void Update()
    {
        if (dead)
        {
            velocity = 0f;
            animatorLight.SetFloat("Speed", Mathf.Abs(velocity));
            animatorDark.SetFloat("Speed", Mathf.Abs(velocity));
            if (!lightHit.isPlaying) { gameObject.SetActive(false); }
            return;
        }
        if (moveTimer > 0f) {
            moveTimer -= Time.deltaTime;
            if (!step.isPlaying) { step.Play(); }
        }
        else
        {
            velocity = 0f;
            if (attack)
            {
                attack = false;
                swing.Play();
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
            float direction = collision.gameObject.transform.position.x - gameObject.transform.position.x;
            if (!attacking)
            {
                if (attack) { return; }
                moveTimer = 1.5f;
                attack = true;
                velocity = Mathf.Sign(direction) * 5f;
            }
            else
            {
                if (playerHit) { return; } playerHit = true;
                axeHit.Play();
                collision.gameObject.GetComponent<PlayerMovement>().animatorLight.SetBool("Dead", true);
                collision.gameObject.GetComponent<PlayerMovement>().animatorDark.SetBool("Dead", true);
                collision.gameObject.GetComponent<PlayerMovement>().enabled = false;
                collision.gameObject.GetComponent<PlayerPowers>().TurnIntoHuman();
                Vector2 knockback = new Vector2(Mathf.Sign(direction) * 400f, 400f);
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(knockback);
                StartCoroutine(RestartLevel());
            }
        }
    }

    IEnumerator RestartLevel()
    {
        float fadeTime = GameObject.Find("Main Camera").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "LightBall")
        {
            lightHit.Play();
            if (health == 5) { healthBar.SetActive(true); }
            health--;
            healthBar.transform.localScale = new Vector3(0.4f * health, 0.2f, 1f);
            if (health == 0)
            {
                dead = true;
            }
        }
        
    }
}
