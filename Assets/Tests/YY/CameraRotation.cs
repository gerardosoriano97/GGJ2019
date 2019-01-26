using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    public float dragSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, Input.GetAxis("RightH") * dragSpeed, 0);

        float rightV = Input.GetAxis("RightV");
        Vector3 rotation = new Vector3(rightV * dragSpeed, 0, 0);
        transform.localEulerAngles += rotation;
        if (transform.localEulerAngles.x < -10.0f || transform.localEulerAngles.x > 40.0f) {
            transform.localEulerAngles -= rotation;
        }
    }
}
