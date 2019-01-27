using System.Collections;
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

    public Animator animator;
    public bool Running { get; private set; } = false;
    public float minRunVal = 0.3f;

    // public List<Color> colors = new List<Color>() {
    //     new Color(1.0f, 0.0f, 0.1411f),
    //     new Color(0.2f, 0.7843f, 0.0f),
    //     new Color(0.8588f, 0.8509f, 0.0f),
    //     new Color(0.0f, 0.4784f, 0.8588f)
    // };

    public float outline = 0.035f;
    public Color outlineColor;

    SpawnVFX spawnVFX;
    public GameObject vfx_shout;

    void Start()
    {
        GetComponentInChildren<Renderer>().material.SetColor("_OutlienColor", outlineColor);

        BearCry.Instance.gaiaCry.AddListener(GaiaAttend);
        BearCry.Instance.outlineIn.AddListener(OutlineIn);
        BearCry.Instance.outlineOut.AddListener(OutlineOut);

        spawnVFX = GetComponent<SpawnVFX>();
    }
    // Update is called once per frame
    void Update()
    {
        if (attending) {
            //Debug.Log(agent.remainingDistance);
            if (agent.remainingDistance <= 1.0f) {
                attending = false;
            }
        }
        UpdateAnimation();
    }
    void OnDestroy() {
        BearCry.Instance.gaiaCry.RemoveListener(GaiaAttend);
    }

    void UpdateAnimation()
    {
        Running = agent.velocity.magnitude > minRunVal;
        animator.SetBool("running", Running);
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

    void OutlineIn() {  
        GetComponentInChildren<Renderer>().material.SetFloat("_Outline", outline);
    }

    void OutlineOut() {
        GetComponentInChildren<Renderer>().material.SetFloat("_Outline", 0.0f);
    }

    IEnumerator AttackRoutine() {
        while (true) {
            if (attending || target == null) yield break;
            //Debug.Log("Attack");
            Attack();
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    public void Attack() {
        AnimalShout.Emmit(transform.gameObject, shout, transform.forward);
        if (spawnVFX != null && vfx_shout != null)
        {
            spawnVFX.Spawn(vfx_shout, transform.position, Quaternion.LookRotation(transform.forward), true);
        }

    }
}
