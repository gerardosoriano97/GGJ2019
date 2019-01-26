using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTable : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    float rotation = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotation += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        //transform.RotateAround( transform.position, Vector3.up, Time.deltaTime);
    }
}
