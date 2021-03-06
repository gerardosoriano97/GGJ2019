﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BearMovement : MonoBehaviour
{
    Rigidbody body;
    public float speed = 2.0f;
    public Vector3 LookDirection { get; private set; } = Vector3.back;
    public float lookThreshold = 0.1f;
    public float slowSpeed = 0.5f;
    public Timer slowTimer;
    public float CurrentSpeed { get; private set; }

    public AudioSourceExtend roarSound;

    public UnityEvent Roar;
    public IEnumerator shoutCooldownRoutine;
    public float shoutCooldown = 3.0f;
    public bool canShout = true;

    public GameObject bearShout;
    public Animator anim;

    public GameObject vfx_shout;
    SpawnVFX spawnVFX;
    public Transform vfx_spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        CurrentSpeed = speed;
        anim = GetComponentInChildren<Animator>();
        spawnVFX = GetComponent<SpawnVFX>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        Movement();
        Look();
    }

    void Movement() {
        Vector3 cameraRight = Camera.main.transform.TransformDirection(Vector3.right);
        Vector3 cameraFront = Camera.main.transform.TransformDirection(Vector3.forward);
        cameraRight.y = 0;
        cameraFront.y = 0;
        cameraRight.Normalize();
        cameraFront.Normalize();
        body.AddForce(cameraFront * Input.GetAxis("Vertical") * CurrentSpeed *body.mass);
        body.AddForce(cameraRight * Input.GetAxis("Horizontal") * CurrentSpeed * body.mass);
        anim.SetFloat("speed", body.velocity.magnitude);
        //transform.LookAt(transform.forward); // now just look at it. (remember that it is assumed up is Vector3.up.
    }
    void CheckInput()
    {
        if (Input.GetAxis("GaiaRoar") > 0.0f && canShout) {
            shoutCooldownRoutine = ShoutCooldown();
            StartCoroutine(shoutCooldownRoutine);
            AnimalShout.Emmit(gameObject, bearShout, LookDirection);
            Roar?.Invoke();
            roarSound.Play();

            if(spawnVFX != null && vfx_shout != null){
                spawnVFX.Spawn(vfx_shout, vfx_spawnPoint.position, Quaternion.LookRotation(LookDirection), true);
            }

            anim.SetTrigger("attacking");
            // VFX SHOUTING????
        }
    }
    void Look()
    {
        if (body.velocity.magnitude > lookThreshold)
            LookDirection = body.velocity;

        LookDirection = new Vector3(LookDirection.x, 0.0f, LookDirection.z).normalized;
        transform.rotation = Quaternion.LookRotation(LookDirection);

        //transform.LookAt(transform.position + LookDirection);
    }

    public void ResetSpeed()
    {
        CurrentSpeed = speed;
    }

    public void SlowDown()
    {
        CurrentSpeed = slowSpeed;
        slowTimer.Restart(this);
    }

    public IEnumerator ShoutCooldown() {
        canShout = false;
        yield return new WaitForSeconds(shoutCooldown);
        canShout = true;
    }
}
