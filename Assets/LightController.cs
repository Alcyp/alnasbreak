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
    public float angle;
    public float rotation;

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
    }

    void Update()
    {
        UpdateCone();
        UpdateShape();
        Rotate();
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
}
