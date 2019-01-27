using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVFX : MonoBehaviour
{

    public GameObject Spawn(GameObject effectToSpawn, Vector3 position, Quaternion orientation, bool autoDestroyOnFinish = true) {
        return DoSpawn(effectToSpawn, position, orientation, autoDestroyOnFinish);
    }

    public GameObject SpawnAsChild(GameObject effectToSpawn, Transform parent, Quaternion orientation, bool autoDestroyOnFinish = true) {
        GameObject vfx = DoSpawn(effectToSpawn, parent.position, orientation, autoDestroyOnFinish);
        if(vfx != null)
            vfx.transform.parent = parent;
        
        return vfx;
    }

    public GameObject SpawnAsChild(GameObject effectToSpawn, Transform parent, Vector3 offset, Quaternion orientation, bool autoDestroyOnFinish = true) {
        GameObject vfx = DoSpawn(effectToSpawn, parent.position + offset, orientation, autoDestroyOnFinish);
        if(vfx != null)
            vfx.transform.parent = parent;
        return vfx;
    }

    private GameObject DoSpawn(GameObject effectToSpawn, Vector3 position, Quaternion orientation, bool autoDestroyOnFinish = true){
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
