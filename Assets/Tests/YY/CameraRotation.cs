using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    public float hSpeed = 2.0f;
    public float vSpeed = 2.0f;
    public float vMin = -10.0f;
    public float vMax = 40.0f;
    // Start is called before the first frame update

    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 1.0f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //transform.eulerAngles += new Vector3(0, Input.GetAxis("RightH") * -hSpeed, 0);
        transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, transform.eulerAngles + new Vector3(0, Input.GetAxis("RightH") * -hSpeed, 0), ref velocity, 1.0f);

        float rightV = Input.GetAxis("RightV");
        Vector3 rotation = new Vector3(0, 0, rightV * vSpeed);
        transform.localEulerAngles += rotation;
        if (transform.localEulerAngles.x < vMin || transform.localEulerAngles.x > vMax) {
            transform.localEulerAngles -= rotation;
        }
    }
}
