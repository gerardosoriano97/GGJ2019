using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GaiaCry : UnityEvent<int>{}

public class BearCry : MonoBehaviour
{
    [SerializeField]
    public GaiaCry gaiaCry;
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

            if (Input.GetAxis("Fire1") > 0.0f) {
                gaiaCry?.Invoke(0);
            }
            if (Input.GetAxis("Fire2") > 0.0f) {
                gaiaCry?.Invoke(1);
            }
            if (Input.GetAxis("Fire3") > 0.0f) {
                gaiaCry?.Invoke(2);
            }
            if (Input.GetAxis("Jump") > 0.0f) {
                gaiaCry?.Invoke(3);
            }
        }
    }

    public IEnumerator BearCryCooldown() {
        yield return new WaitForSeconds(cryCooldown);
        onCooldown = false;
    }
}
