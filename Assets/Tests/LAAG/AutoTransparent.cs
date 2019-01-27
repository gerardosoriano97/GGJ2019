using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTransparent : MonoBehaviour
{
    MeshRenderer mr;
    Material transparentMaterial;
    float transparency = 0.0f;
    float transparencyTarget = 0.0f;

    [HideInInspector] 
    public bool isTransparent;

    public float transitionSpeed = 20.0f;


    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponentInChildren<MeshRenderer>();
        transparentMaterial = mr.material;

    }

    // Update is called once per frame
    void Update()
    {
        if(mr == null)
            return;

        if(transparency != transparencyTarget) {
            transparency = Mathf.Lerp(transparency, transparencyTarget, transitionSpeed * Time.deltaTime );
            transparentMaterial.SetFloat("_useFade", transparency);            
        }

    }

    public void EnableTransparency( float distance ) {
        if( hasFadingMaterial()){
            Material transparent = mr.material;
            transparencyTarget = 1.0f;
            transparent.SetFloat("_DistanceOffset", distance);
            isTransparent = true;
        }
    }

    public void DisableTransparency() {
        if( hasFadingMaterial()){
            Material transparent = mr.material;
            transparencyTarget = 0.0f;
            isTransparent = false;
        }
    }

    private bool hasFadingMaterial() {
        bool result = false;
        if(mr != null) {
            Material transparent = mr.material;
            if(transparent != null && transparent.HasProperty("_useFade"))
            {
                result = true;
            }
            else
            {
                Debug.Log("No usable material");
            }
        }
        return result;
    }
}
