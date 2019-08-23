using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0 : MonoBehaviour
{
    public LightController light1;
    [Range(0f,100f)]
    public float swingSpeed = 0f;
    [Range(0f,360f)]
    public float swingAngle = 0f;
    float startingAngle;
    bool swingDirection = false;

    // Start is called before the first frame update
    void Start()
    {
        startingAngle = light1.angle;
    }

    // Update is called once per frame
    void Update()
    {
        if (swingDirection) { light1.angle += swingSpeed * Time.deltaTime; }
        else { light1.angle -= swingSpeed * Time.deltaTime; }
        if (light1.angle > startingAngle + swingAngle) { swingDirection = !swingDirection; }
        if (light1.angle < startingAngle - swingAngle) { swingDirection = !swingDirection; }
    }
}
