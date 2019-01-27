﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public Shaker shaker;
    new public Rigidbody rigidbody;
    public int health = 3;
    public float dieForce = 1.0f;
    public Vector3 dieForceOffset = Vector3.zero;
    public Collider trigger;
    public Timer dieTimer;
    // Start is called before the first frame update
    void Start()
    {
        if (rigidbody == null) {
            rigidbody = GetComponent<Rigidbody>();
        }
        this.rigidbody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void Hit(Lumberjack lumberjack)
    {

        // VFX RED REACTION

        health--;
        if(health == 0)
        {
            Fall(lumberjack);
        }
        else if(health > 0){
            shaker?.Shake();
        }
    }

    void Fall(Lumberjack lumberjack)
    {
        Destroy(trigger);
        DestroyImmediate(shaker);

        // VFX PARTICLES LEAFS

        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        var sideVec = transform.position - lumberjack.transform.position;
        sideVec.y = 0.0f;
        sideVec.Normalize();
        rigidbody.AddForceAtPosition(sideVec * dieForce, dieForceOffset, ForceMode.Impulse);
        dieTimer.Start(this);
    }

    public bool IsDead
        => this.health < 0;
}
