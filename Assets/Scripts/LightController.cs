﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    Texture2D texture;
    float width;
    public bool turnedOn;
    public bool changeDirection;
    public float lightDistance;
    SpriteRenderer ren;
    Sprite sprite;
    public float rotation;
    public bool continuousRotation = false;
    public float angle;
    [Range(0, 100f)]
    public float swingSpeed = 0f;
    [Range(0f, 360f)]
    public float swingAngle = 0f;
    [Range(0f, 30f)]
    public float widenSpeed = 0f;
    float startingAngle;
    float startingRotation;
    float startingLightDistance;
    float swingDirection = 1f;
    float widenDirection = 1f;

    public bool blink;
    [Range(1f, 10f)]
    public float onTime;
    [Range(1f, 10f)]
    public float offTime;
    float blinkTimer = 0f;

    void OnValidate()
    {
        ren = GetComponent<SpriteRenderer>();
        UpdateCone();
        ApplyRotation();
    }

    void Awake()
    {
        ren = GetComponent<SpriteRenderer>();
        startingAngle = angle;
        startingRotation = rotation;
        startingLightDistance = lightDistance;
        if (!turnedOn) { lightDistance = 0f; angle = 0f; }
        UpdateCone();
    }

    void Update()
    {
        if (turnedOn) { lightDistance = startingLightDistance; }
        
        //lightDistance = turnedOn ? startingLightDistance : 0f;

        UpdateCone();
        Rotate();
        MoveLight();
        Blink();
    }

    void UpdateCone()
    {
        float angleRad = (angle / 2f) * Mathf.PI / 180f;
        width = Mathf.Sin(angleRad) * lightDistance / Mathf.Cos(angleRad);
        if (transform.localScale.x == width && transform.localScale.y == lightDistance) { return; }
        transform.localScale = new Vector3(width, lightDistance, 1f);
    }

    void Rotate()
    {
        if (!continuousRotation)
        {
            if (rotation > startingRotation + swingAngle) { swingDirection = -1f; }
            if (rotation < startingRotation - swingAngle) { swingDirection = 1f; }
        }
        float swingD = swingDirection;
        if (changeDirection) { swingD *= -1f; }
        rotation += swingSpeed * Time.deltaTime * swingD;
        ApplyRotation();
    }

    void ApplyRotation()
    {
        if (rotation == transform.eulerAngles.z) { return; }
        Vector3 temp = transform.eulerAngles;
        temp.z = rotation;
        transform.eulerAngles = temp;
    }

    void MoveLight()
    {
        if (!turnedOn)
        {
            widenDirection = -1f;
            if (angle <= 0f) { lightDistance = 0f; return; }
        }
        angle += widenSpeed * Time.deltaTime * widenDirection;
        if (angle > startingAngle) { widenDirection = -1f; }
        if (angle < startingAngle) { widenDirection = 1f; }
        if (Mathf.Abs(angle) > (startingAngle + 0.1f)) { widenDirection = 0f; }
    }

    void Blink()
    {
        if (!blink) { return; }
        if (turnedOn)
        {
            if (blinkTimer >= onTime) { turnedOn = false; blinkTimer = 0f;  return; }
        }
        else
        {
            if (blinkTimer >= offTime) { turnedOn = true; blinkTimer = 0f; return; }
        }
        blinkTimer += Time.deltaTime;
    }

    public void turnOn()
    {
        turnedOn = true;
    }

    public void turnOff()
    {
        turnedOn = false;
    }
}
