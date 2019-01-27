using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public bool isLocal = false;
    public float duration = 1.0f;
    public float peakIntensity = 1.0f;
    public float GoalIntensity { get; private set; } = 0.0f;
    public float CurrentIntensity { get; private set; } = 0.0f;
    public Vector3 OriginalPos { get; private set; } = Vector3.zero;

    float startTime = 0.0f;
    private void Awake()
    {
        if(!isLocal)
            OriginalPos = transform.position;
        else
            OriginalPos = transform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float t = (Time.time - startTime) / duration;
        CurrentIntensity = Mathf.SmoothStep(CurrentIntensity, 0.0f, t);
        if(!isLocal)
            transform.position = OriginalPos + NextShake;
        else
            transform.localPosition = OriginalPos + NextShake;
    }

    public void Shake()
    {
        startTime = Time.time;
        CurrentIntensity = peakIntensity;
    }

    //private Vector3 Vibrate()
    //{
    //}

    public Vector3 NextShake
        =>  new Vector3(
            Random.Range(-CurrentIntensity, CurrentIntensity),
            Random.Range(-CurrentIntensity, CurrentIntensity),
            Random.Range(-CurrentIntensity, CurrentIntensity)
            );  
    
}
