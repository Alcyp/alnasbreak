using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    Texture2D texture;
    float width;
    public bool turnedOn;
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
    [Range(0f, 10f)]
    public float widenSpeed = 0f;
    [Range(0f, 45f)]
    public float widenAngle = 0f;
    float startingAngle;
    float startingRotation;
    float startingLightDistance;
    float swingDirection = 1f;
    float widenDirection = 1f;

    void OnValidate()
    {
        ren = GetComponent<SpriteRenderer>();
        UpdateCone();
        Rotate();
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
        if (turnedOn) { turnOn(); }
        if (!turnedOn) { return; }
        UpdateCone();
        Rotate();
        MoveLight();
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
        if (rotation == transform.eulerAngles.z) { return; }
        Vector3 temp = transform.eulerAngles;
        temp.z = rotation;
        transform.eulerAngles = temp;
    }

    void MoveLight()
    {
        rotation += swingSpeed * Time.deltaTime * swingDirection;
        if (continuousRotation) { return; }
        if (rotation > startingRotation + swingAngle) { swingDirection = -1f; }
        if (rotation < startingRotation - swingAngle) { swingDirection = 1f; }

        angle += widenSpeed * Time.deltaTime * widenDirection;
        if (angle > startingAngle + widenAngle) { widenDirection = -1f; }
        if (angle < startingAngle - widenAngle) { widenDirection = 1f; }
    }

    public void turnOn()
    {
        turnedOn = true;
        lightDistance = startingLightDistance;
    }
}
