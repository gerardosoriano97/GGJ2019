using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVFX : MonoBehaviour
{
    public GameObject effectToSpawn = null;
    //public float destroyAfter = 1.0f;

    public GameObject Spawn(Vector3 position, Quaternion orientation) {
        GameObject vfx = null;
        if( effectToSpawn != null ) {
            vfx = Instantiate( effectToSpawn, position, orientation);
            ParticleSystem ps = vfx.transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(vfx, ps.main.duration);
        }
        else {
            Debug.Log( "No effect to spawn" );
        }

        return vfx;
    }

}
