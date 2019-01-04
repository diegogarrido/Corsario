using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public float speed;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (moveHorizontal != 0)
        {
            rb.AddForce(transform.right * moveHorizontal * speed * Time.deltaTime);
        }
        if (moveVertical != 0)
        {
            rb.AddForce(transform.up * moveVertical * speed * Time.deltaTime);
        }
    }
}
