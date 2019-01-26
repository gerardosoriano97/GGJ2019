using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BearMovement : MonoBehaviour
{
    Rigidbody body;
    public float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();        
    }

    void Movement() {
        Vector3 cameraRight = Camera.main.transform.TransformDirection(Vector3.right);
        Vector3 cameraFront = Camera.main.transform.TransformDirection(Vector3.forward); 
        cameraRight.y = 0;
        cameraFront.y = 0;
        cameraRight.Normalize();
        cameraFront.Normalize();
        body.AddForce(cameraFront * Input.GetAxis("Vertical") * speed);
        body.AddForce(cameraRight * Input.GetAxis("Horizontal") * speed);

        transform.LookAt(transform.forward); // now just look at it. (remember that it is assumed up is Vector3.up.
    }
}
