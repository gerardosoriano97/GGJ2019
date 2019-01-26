using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lumberjack : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector3 goal;
    public static Lumberjack Instance { get; private set; }
    public Tree Target { get; private set; } = null;

    public bool Flee { get; private set; } = false;
    public bool Scared { get; private set; } = false;

    public float velocityMultiplier = 0.5f;

    public Ticker attackTimer;

    public Animator animator;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        attackTimer.Start(this);
    }

    public void AttackTree()
    {
        if (HaveTarget)
        {
            Target.Hit(this);
            if (Target.IsDead)
            {
                GotoCenter();
            }
            //Target.shaker.Shake();
        }
        //Debug.Log("Ataque al arbol...");
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            if (Target.IsDead)
            {
                Target = null;
                GotoCenter();
            }
        }
        else
        {
            GotoCenter();
        }
        agent.SetDestination(goal);
        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        float agentSpeed = agent.velocity.magnitude;
        // agent.velocity.magnitude
        animator.SetFloat("speed", agentSpeed);
        animator.SetBool("scared", Scared);
        animator.SetBool("flee", Flee);
        animator.SetFloat("speedMultiplier", 0.5f + agent.velocity.magnitude * velocityMultiplier);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!HaveTarget)
        {
            if (other.gameObject.tag == "Tree")
            {
                var tar = other.gameObject.GetComponent<Tree>();
                if (!tar.IsDead) {
                    Target = tar;
                    goal = other.gameObject.transform.position;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (HaveTarget)
        {
            if (other.gameObject.tag == "Tree")
            {
                //si el objeto que deje es el target, setearlo a null...
                if (other.gameObject.GetComponent<Tree>() == Target) {
                    Target = null;
                    GotoCenter();
                }
            }
        }
    }

    public void GotoCenter()
    {
        goal = MainTree.Center;
    }

    public bool HaveTarget
        => Target != null;
}
