using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSpawnExample : MonoBehaviour
{

    public Transform target;
    public bool destroyOnFinish = true;
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

            float offsetFromCenter = 0.5f;
            Vector3 dir = (transform.position - target.position).normalized;
            Vector3 position = target.position + (dir * offsetFromCenter);

            spawner.Spawn(effectToSpawn, position, Quaternion.LookRotation(dir), destroyOnFinish);
        }
    }
}
