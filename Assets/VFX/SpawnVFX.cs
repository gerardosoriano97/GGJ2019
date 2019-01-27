using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVFX : MonoBehaviour
{
    public GameObject effectToSpawn = null;

    public GameObject Spawn(Vector3 position, Quaternion orientation, bool autoDestroyOnFinish = true) {
        return DoSpawn(position, orientation, autoDestroyOnFinish);
    }

    public GameObject SpawnAsChild(Transform parent, Quaternion orientation, bool autoDestroyOnFinish = true) {
        GameObject vfx = DoSpawn(parent.position, orientation, autoDestroyOnFinish);
        vfx.transform.parent = parent;
        return vfx;
    }

    public GameObject SpawnAsChild(Transform parent, Vector3 offset, Quaternion orientation, bool autoDestroyOnFinish = true) {
        GameObject vfx = DoSpawn(parent.position + offset, orientation, autoDestroyOnFinish);
        vfx.transform.parent = parent;
        return vfx;
    }

    private GameObject DoSpawn(Vector3 position, Quaternion orientation, bool autoDestroyOnFinish = true){
        GameObject vfx = null;
        if( effectToSpawn != null ) {
            vfx = Instantiate( effectToSpawn, position, orientation);
            ParticleSystem ps = vfx.transform.GetChild(0).GetComponent<ParticleSystem>();
            if(autoDestroyOnFinish){
                Destroy(vfx, ps.main.duration);
            }
        }
        else {
            Debug.Log( "No effect to spawn" );
        }
        return vfx; 
    }

}
