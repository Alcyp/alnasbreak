using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    senseLogic sense;
    public bool inLight;

    private void Awake()
    {
        sense = GetComponentInChildren<senseLogic>();
    }

    void Update()
    {
        inLight = sense.inLight;
    }
}
