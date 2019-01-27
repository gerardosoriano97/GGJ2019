using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSpawnExample : MonoBehaviour
{

    public Transform target;
    public GameObject effectToSpawn;
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
            spawner.SpawnAsChild(effectToSpawn, target, Quaternion.identity, false);
        }
    }
}
