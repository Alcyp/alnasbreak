using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWhenKilledGuard : MonoBehaviour
{

    public GameObject guard;
    public GameObject door;

    void Start()
    {
        door.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (guard.activeSelf == false)
        {
            door.SetActive(true);
            gameObject.SetActive(false);
            
        }
    }
}
