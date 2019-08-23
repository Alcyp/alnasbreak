using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    Texture2D texture;
    float width;
    public float lightDistance;
    SpriteRenderer ren;
    Sprite sprite;
    public float rotation;
    public float angle;
    public bool continuousRotation = false;
    [Range(0, 100f)]
    public float swingSpeed = 0f;
    [Range(0f, 360f)]
    public float swingAngle = 0f;
    [Range(0f, 10f)]
    public float widenSpeed = 0f;
    [Range(0f, 45f)]
    public float widenAngle = 0f;
    public float startingAngle;
    public float startingRotation;
    float swingDirection = 1f;
    float widenDirection = 1f;

    void OnValidate()
    {
        ren = GetComponent<SpriteRenderer>();
        UpdateCone();
        UpdateShape();
        Rotate();
    }

    void Awake()
    {
        ren = GetComponent<SpriteRenderer>();
        startingAngle = angle;
        startingRotation = rotation;
    }

    void Update()
    {
        UpdateCone();
        UpdateShape();
        Rotate();
        MoveLight();
    }

    void UpdateShape()
    {
        if (transform.localScale.x == width && transform.localScale.y == lightDistance) { return; }
        transform.localScale = new Vector3(width, lightDistance, 1f);
    }

    void UpdateCone()
    {
        float angleRad = (angle / 2f) * Mathf.PI / 180f;
        width = Mathf.Sin(angleRad) * lightDistance / Mathf.Cos(angleRad);
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
        if (rotation > startingRotation + swingAngle) { swingDirection *= -1f; }
        if (rotation < startingRotation - swingAngle) { swingDirection *= -1f; }

        angle += widenSpeed * Time.deltaTime * widenDirection;
        if (angle > startingAngle + widenAngle) { widenDirection *= -1f; }
        if (angle < startingAngle - widenAngle) { widenDirection *= -1f; }
    }
}
