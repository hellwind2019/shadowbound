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
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float rotationSpeed = 5f;
    private int previousDirection = 0;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            direction = -1;
            previousDirection = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = 1;
            previousDirection = 1;
        }
        else direction = 0;

        if (Math.Abs(rb.velocity.x) > maxSpeedX) rb.velocity = new Vector3(Math.Sign(rb.velocity.x) * maxSpeedX, rb.velocity.y, rb.velocity.z);

        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
        if (direction != 0)
        {
            RotatePlayer(direction);
        }
        else
        {
            float targetAngle = previousDirection == -1 ? 180f : 0f;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }


    }

    private void RotatePlayer(int direction)
    {
        float targetAngle = direction == -1 ? 180f : 0f;
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);

    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector3(direction * speed, 0, 0));
    }
}
