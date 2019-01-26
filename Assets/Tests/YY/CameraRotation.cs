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
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, Input.GetAxis("RightH") * -hSpeed, 0);

        float rightV = Input.GetAxis("RightV");
        Vector3 rotation = new Vector3(rightV * vSpeed, 0, 0);
        transform.localEulerAngles += rotation;
        if (transform.localEulerAngles.x < vMin || transform.localEulerAngles.x > vMax) {
            transform.localEulerAngles -= rotation;
        }
    }
}
