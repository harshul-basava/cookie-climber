using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_Controller : MonoBehaviour
{
    float true_speed = 6f;
    public float jump;
    public float speed;
    public float drag;
    
    Vector3 pos;

    public bool grounded = true;
    public bool attached = false;
    public bool hanging = false;

    public GameObject curr;
    public Rigidbody2D rb;
    public Rigidbody2D left;
    public Rigidbody2D right;

    public HingeJoint2D rightHinge;
    public HingeJoint2D leftHinge;
    
    void Start()
    {
        pos = transform.position;
        EventManager._Jump += Jump;
        EventManager._Drop += Drop;
        EventManager._Grab += Grab;
        EventManager._Right += Right;
        EventManager._Left += Left;
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space)) 
        // {
        //     Jump();
        //     Grab();
        // }
        
        // if (Input.GetKeyDown(KeyCode.DownArrow))
        // {
        //     Drop();
        // }

        // if (Input.GetKey(KeyCode.RightArrow))
        // {
        //     Right();
        // }

        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     Left();
        // }

        Vector3 velocity = rb.velocity;
        velocity.x = velocity.x / (drag + 1f); 
        rb.velocity = velocity;  

        if (!grounded) 
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 5f*Time.deltaTime);
        }

        if (curr && curr.GetComponent<HingeJoint2D>().connectedBody != null)
        {
            grounded = true;
        }
    }

    void FixedUpdate() 
    {
        Horizontal();
    }

    void Drop()
    {
        if (grounded && curr && curr.GetComponent<HingeJoint2D>().connectedBody) 
        {
            curr.GetComponent<HingeJoint2D>().connectedBody = null;
            rightHinge.anchor = new Vector2(0.5f, -0.3f);
            leftHinge.anchor = new Vector2(-0.5f, -0.3f);
            grounded = false;
            hanging = false;
        }
    }

    void Jump() 
    {
        if (grounded) 
        {
            grounded = false;
            if (curr && curr.GetComponent<HingeJoint2D>().connectedBody) 
            {
                curr.GetComponent<HingeJoint2D>().connectedBody = null;
                rightHinge.anchor = new Vector2(0.5f, -0.3f);
                leftHinge.anchor = new Vector2(-0.5f, -0.3f);
                hanging = false;
            }
            rb.velocity = new Vector2(0 * gameObject.GetComponent<Rigidbody2D>().velocity.x, 0f);
            rb.AddForce(new Vector2(0f, jump * 1f), ForceMode2D.Impulse);
        }
    }

    void Grab()
    {
        if (attached && curr.GetComponent<HingeJoint2D>().connectedBody == null)
        {
            if (Vector2.Distance(curr.transform.position, left.transform.position) > Vector2.Distance(curr.transform.position, right.transform.position))
            {
                rightHinge.anchor = new Vector2(0.5f, 0.3f);
                curr.GetComponent<HingeJoint2D>().connectedBody = right;
            }
            else
            {
                leftHinge.anchor = new Vector2(-0.5f, 0.3f);
                curr.GetComponent<HingeJoint2D>().connectedBody = left;
            }

            hanging = true;
            attached = false;
            grounded = true;
        }
    }

    void Left()
    {
        drag = 0.11f;
        rb.velocity = new Vector2(-1f * true_speed, rb.velocity.y);
    }

    void Right()
    {
        drag = 0.11f;
        rb.velocity = new Vector2(true_speed, rb.velocity.y);
    }

    void Horizontal()
    {
        // if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        // {
        //     drag = 0.11f;
        //     if (rb.velocity.x < 6.5f)
        //     {
        //         rb.velocity = new Vector2(rb.velocity.x + (Time.fixedDeltaTime * (speed/20)), rb.velocity.y);
        //     } else {
        //         rb.velocity = new Vector2(6.5f, rb.velocity.y);
        //     }  
        // } 
        // else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        // {
        //     drag = 0.11f;
        //     if (rb.velocity.x > -6.5f)
        //     {
        //         rb.velocity = new Vector2(rb.velocity.x - (Time.fixedDeltaTime * (speed/20)), rb.velocity.y);
        //     } else {
        //         rb.velocity = new Vector2(-6.5f, rb.velocity.y);
        //     }  
        // }
        // else
        // {
        //     drag = 0.5f;
        // }

        // if (rb.velocity.y < -30)
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, -30);
        // }

        // Vector3 velocity = rb.velocity;
        // velocity.x = velocity.x / (drag + 1f); 
        // rb.velocity = velocity;  
    }

  
    void OnCollisionEnter2D(Collision2D collision) //ground check
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) //hold enter check
    {
        if (collision.gameObject.tag == "Hold" && !hanging)
        {
            curr = collision.gameObject;
            attached = true;
        }

        if (collision.gameObject.tag == "Danger")
        {
            if (curr && curr.GetComponent<HingeJoint2D>().connectedBody) 
            {
                curr.GetComponent<HingeJoint2D>().connectedBody = null;
                rightHinge.anchor = new Vector2(0.5f, -0.3f);
                leftHinge.anchor = new Vector2(-0.5f, -0.3f);
                hanging = false;
            }
            rb.velocity = Vector3.zero;
            transform.position = pos;
        }

        if (collision.gameObject.tag == "Key")
        {
            Destroy(collision.gameObject.GetComponent<door>().connectedDoor);
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collision) //hold enter check
    {
        if (collision.gameObject.tag == "Hold" && !hanging)
        {
            curr = collision.gameObject;
            attached = true;
        }

    }

    void OnTriggerExit2D(Collider2D collision) //hold leave check
    {
        if (collision.gameObject.tag == "Hold")
        {
            attached = false;
        }

    }

    void OnCollisionExit2D(Collision2D collision) //ground check
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}
