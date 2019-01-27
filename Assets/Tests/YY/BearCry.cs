using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GaiaCry : UnityEvent<int>{}

[System.Serializable]
public class OutlineIn : UnityEvent {}

[System.Serializable]
public class OutlineOut : UnityEvent {}

public class BearCry : MonoBehaviour
{
    [SerializeField]
    public GaiaCry gaiaCry;
    [SerializeField]
    public OutlineIn outlineIn;
    [SerializeField]
    public OutlineOut outlineOut;

    public static BearCry Instance {get; private set;}
    public IEnumerator bearCryCooldown;
    private bool onCooldown = false;
    public float cryCooldown = 3.0f;
    void Awake(){
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bearCryCooldown = BearCryCooldown();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetAxis("GaiaCry") > 0.0f && !onCooldown) {
        //     onCooldown = true;
        //     bearCryCooldown = BearCryCooldown();
        //     StartCoroutine(bearCryCooldown);
        //     GaiaCry?.Invoke();
        // }

        if (Input.GetAxis("GaiaCry") > 0.0f) {

            // VFX SHOW OUTLINE ON ENTITIES
            outlineIn?.Invoke();

            if (Input.GetAxis("Fire1") > 0.0f) {
                //Sonido de venado
                gaiaCry?.Invoke(0);
            }
            if (Input.GetAxis("Fire2") > 0.0f) {
                //Sonido de venado
                gaiaCry?.Invoke(1);
            }
            if (Input.GetAxis("Fire3") > 0.0f) {
                //Sonido de mapache
                gaiaCry?.Invoke(2);
            }
            if (Input.GetAxis("Jump") > 0.0f) {
                //Sonido de mapache
                gaiaCry?.Invoke(3);
            }
        } else {
            outlineOut?.Invoke();
        }
    }

    public IEnumerator BearCryCooldown() {
        yield return new WaitForSeconds(cryCooldown);
        onCooldown = false;
    }
}
