using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class senseLogic : MonoBehaviour
{
    public bool inLight;
    SpriteRenderer glow;

    private void Awake()
    {
        glow = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Entering " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Light") {
            inLight = true;
            glow.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Leaving " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Light") {
            inLight = false;
            glow.enabled = false;
        }
    }
}
