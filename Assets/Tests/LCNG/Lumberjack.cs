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
    public float scareTime = 1.0f;

    public bool Flee { get; private set; } = false;
    public bool Scared { get; private set; } = false;

    public float velocityMultiplier = 0.5f;

    public Ticker attackTimer;
    public Timer scaredTimer;

    public Animator animator;

    public Vector3 ScaredGoal { get; private set; }
    Vector3 currentScaredVelocity = Vector3.zero;

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
        transform.LookAt(goal);
        UpdateScaring();
        UpdateAnimator();
    }
    void UpdateScaring()
    {
        if (Scared) {
            agent.Warp(
                Vector3.SmoothDamp(
                    transform.position, 
                    ScaredGoal, 
                    ref currentScaredVelocity, 
                    1.0f
                    )
                 );

            if (Vector3.Distance(transform.position, ScaredGoal) < 0.1f)
            {
                ResetToNormal();
            }
        }
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

    public void Push(AnimalShout shout)
    {
        if (!Scared) {
            Scared = true;
            scaredTimer.Start(this);
            ScaredGoal = transform.position + (shout.Direction * shout.pushForce);
        }
    }

    public void ResetToNormal()
    {
        currentScaredVelocity = Vector3.zero;
        Scared = false;
    }

    public bool HaveTarget
        => Target != null;
}
