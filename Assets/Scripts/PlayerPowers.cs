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
    public SpriteRenderer playerGlow;
    public SpriteRenderer playerBall;

    private void Awake()
    {
        sense = GetComponentInChildren<SenseLogic>();
        ray = GetComponentInChildren<LightController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inLight = sense.inLight;
        if (!inLight) {
            ray.turnedOn = false;
            return;
        }
        if (Input.GetAxis("Submit") == 1f)
        {
            ray.turnedOn = true;
            ray.rotation = AngleTowardsMouse();
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Ding!");
                //Change shape into light

                rb.AddForce(VectorTowardsMouse() * lightForce);
            }
        }
        else
        {
            ray.turnedOn = false;
        }

    }

    void ToggleBall()
    {
        if (playerBall.enabled)
        {
            playerBall.enabled = false;
            playerLight.enabled = true;
            playerDark.enabled = true;
            playerGlow.enabled = true;
            return;
        }
        playerBall.enabled = true;
        playerLight.enabled = false;
        playerDark.enabled = false;
        playerGlow.enabled = false;
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
