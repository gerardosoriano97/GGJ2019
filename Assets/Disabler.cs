using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabler : MonoBehaviour
{
    public MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
