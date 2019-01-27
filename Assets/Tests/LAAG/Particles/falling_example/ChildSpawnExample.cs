using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSpawnExample : MonoBehaviour
{

    public Transform target;
    SpawnVFX spawner;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponent<SpawnVFX>();
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetMouseButtonDown(0) ) {
            spawner.SpawnAsChild(target, Quaternion.identity, false);
        }
    }
}
