using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Start is called before the first frame update
    private Rigidbody rb;
    private int direction = 0;
    [SerializeField] float speed;
    [SerializeField] float maxSpeedX;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)) direction = -1;
        else if(Input.GetKey(KeyCode.D)) direction = 1;
        else direction = 0;
        if(Math.Abs(rb.velocity.x) > maxSpeedX) rb.velocity = new Vector3(Math.Sign(rb.velocity.x) * maxSpeedX, rb.velocity.y, rb.velocity.z);
        
    }
    void FixedUpdate()
    {
        rb.AddForce(new Vector3(direction * speed, 0, 0));
    }
}
