using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseLogic : MonoBehaviour
{
    public bool inLight;
    public GameObject glow;

    private void Awake()
    {
        glow.transform.localScale = new Vector3(0f, 0f, 1f);
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Entering " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Light") {
            inLight = true;
            glow.transform.localScale = new Vector3(3f, 3f, 1f);
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
