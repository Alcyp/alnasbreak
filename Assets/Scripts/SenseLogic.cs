using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseLogic : MonoBehaviour
{
    public bool inLight;
    public float rotationSpeed;
    GameObject glow;
    float timer = 0f;

    private void Awake()
    {
        glow = GameObject.Find("Sense");
        glow.transform.localScale = new Vector3(0f, 0f, 1f);
    }

    private void Update()
    {
        //glow.transform.Rotate(new Vector3(0f, 0f, rotationSpeed * Time.deltaTime));
        timer += Time.deltaTime;
        if (timer > 0.2f) {
            inLight = false;
            glow.transform.localScale = new Vector3(0f, 0f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Entering " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Light") {
            inLight = true;
            glow.transform.localScale = new Vector3(3f, 3f, 1f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Light")
        {
            inLight = true;
            glow.transform.localScale = new Vector3(3f, 3f, 1f);
            timer = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Leaving " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Light") {
            inLight = false;
            glow.transform.localScale = new Vector3(0f, 0f, 1f);
        }
    }
}
