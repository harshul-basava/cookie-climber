using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horizontalPlatform : MonoBehaviour
{
    public float speed;
    public float amplitude;
    float pos;

    void Start ()
    {
        pos = transform.position.x;
    }

    void Update()
    {
        transform.position = new Vector3(pos + (amplitude * Mathf.Sin(Time.time*speed)), transform.position.y, transform.position.z);
    }
}
