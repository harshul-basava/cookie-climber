using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudFloat : MonoBehaviour
{
    public float speed;
    public float amplitude;
    float pos;

    void Start ()
    {
        pos = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, pos + (amplitude * Mathf.Sin(Time.time*speed)), transform.position.z);
    }
}
