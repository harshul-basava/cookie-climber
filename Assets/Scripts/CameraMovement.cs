using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Transform finish;
    public float upper;
    public float lower;

    void Update()
    {
        if (player.position.x > upper)
        {
            gameObject.transform.position = new Vector3 (upper, Mathf.Min(player.position.y + 1f, finish.position.y), gameObject.transform.position.z);
        } 
        else if (player.position.x < lower)
        {
            gameObject.transform.position = new Vector3 (lower, Mathf.Min(player.position.y + 1f, finish.position.y), gameObject.transform.position.z);
        }
        else
        {
            gameObject.transform.position = new Vector3 (player.position.x, Mathf.Min(player.position.y + 1f, finish.position.y), gameObject.transform.position.z);
        }
        
    }
}
