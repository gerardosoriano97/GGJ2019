using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTree : Tree
{
    public static MainTree Instance { get; private set; }
    public static Vector3 Center { get; private set; } = Vector3.zero;
    private void Awake()
    {
        Instance = this;
        //Center = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
