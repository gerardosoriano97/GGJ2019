﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    public NavMeshAgent agent;
    public enum GaiaSpecie {
        Specie1,
        Specie2,
        Specie3,
        Specie4
    };

    public bool attending = false;

    public GaiaSpecie specie;
    public IEnumerator attackRoutine;
    public GameObject target = null;
    public float attackSpeed = 1.0f;
    public float knockback = 5.0f;

    public GameObject shout;

    void Start()
    {
        BearCry.Instance.gaiaCry.AddListener(GaiaAttend);
    }
    // Update is called once per frame
    void Update()
    {
        if (attending) {
            Debug.Log(agent.remainingDistance);
            if (agent.remainingDistance <= 1.0f) {
                attending = false;
            }
        }
    }
    void OnDestroy() {
        BearCry.Instance.gaiaCry.RemoveListener(GaiaAttend);
    }

    void OnTriggerEnter(Collider collider) {
        Lumberjack lumber = collider.gameObject.GetComponent<Lumberjack>();
        if (lumber != null) {
            target = lumber.gameObject;
            attackRoutine = AttackRoutine();
            StartCoroutine(attackRoutine);
            transform.LookAt(lumber.transform.position);
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.gameObject == target) {
            target = null;
            StopCoroutine(attackRoutine);
        }
    }

    void GaiaAttend(int specieIndex) {
        if ((int)specie == specieIndex) {
            agent.SetDestination(BearCry.Instance.transform.position);
            attending = true;
        }
    }

    IEnumerator AttackRoutine() {
        while (true) {
            if (attending || target == null) yield break;
            Debug.Log("Attack");
            Attack();
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    public void Attack() {
        AnimalShout.Emmit(transform.gameObject, shout, transform.forward);
    }
}