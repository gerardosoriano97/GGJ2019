﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BearMovement : MonoBehaviour
{
    Rigidbody body;
    public float speed = 2.0f;
    public Vector3 LookDirection { get; private set; } = Vector3.back;
    public float lookThreshold = 0.1f;
    public float slowSpeed = 0.5f;
    public Timer slowTimer;
    public float CurrentSpeed { get; private set; }

    public GameObject bearShout;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        CurrentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        Movement();
        Look();
    }

    void Movement() {
        Vector3 cameraRight = Camera.main.transform.TransformDirection(Vector3.right);
        Vector3 cameraFront = Camera.main.transform.TransformDirection(Vector3.forward);
        cameraRight.y = 0;
        cameraFront.y = 0;
        cameraRight.Normalize();
        cameraFront.Normalize();
        body.AddForce(cameraFront * Input.GetAxis("Vertical") * CurrentSpeed);
        body.AddForce(cameraRight * Input.GetAxis("Horizontal") * CurrentSpeed);
        //transform.LookAt(transform.forward); // now just look at it. (remember that it is assumed up is Vector3.up.
    }
    void CheckInput()
    {
        if (Input.GetAxis("GaiaRoar") > 0.0f) {
            AnimalShout.Emmit(gameObject, bearShout, LookDirection);
        }
    }
    void Look()
    {
        if (body.velocity.magnitude > lookThreshold)
            LookDirection = body.velocity;

        transform.LookAt(transform.position + LookDirection);
    }

    public void ResetSpeed()
    {
        CurrentSpeed = speed;
    }

    public void SlowDown()
    {
        CurrentSpeed = slowSpeed;
        slowTimer.Restart(this);
    }
}