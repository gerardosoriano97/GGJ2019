using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class Lumberjack : MonoBehaviour
{
    public UnityEvent OnAttackTree;
    public AudioSource hitSound;
    public NavMeshAgent agent;
    public Vector3 goal;
    public static Lumberjack Instance { get; private set; }
    public Tree Target { get; private set; } = null;
    public float scareTime = 1.0f;

    public bool Flee { get; private set; } = false;
    public bool Scared { get; private set; } = false;

    public float velocityMultiplier = 0.5f;
    public float hp = 5;
    public float bearDmg = 2;
    public float animalDmg = 1;

    public Ticker attackTimer;
    public Timer scaredTimer;

    public Animator animator;

    public Vector3 ScaredGoal { get; private set; }
    Vector3 currentScaredVelocity = Vector3.zero;

    Vector3 initialCenter;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        attackTimer.Start(this);
        initialCenter = MainTree.Center + new Vector3(
            Random.Range(-1.0f,1.0f),
            Random.Range(-1.0f,1.0f),
            Random.Range(-1.0f,1.0f)
            );
    }

    public void AttackTree()
    {
        if (HaveTarget)
        {
            if (!Scared)
            {
                if (Target.CanPlaySound)
                {
                    hitSound.pitch = Random.Range(1.0f, 2.0f);
                    hitSound.Play();
                }
                Target.Hit(this);
                OnAttackTree?.Invoke();
            }
            if (Target.IsDead)
            {
                GotoCenter();
                animator.SetBool("cutting", false);
            }

            // VFX HIT PARTICLES

            

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

        if (Target != null && agent.remainingDistance <= 0.0f) {
            animator.SetBool("cutting", true);
        }
        //transform.LookAt(goal);
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
        goal = initialCenter;
    }

    public void Push(AnimalShout shout)
    {
        if (!Scared) {
            Scared = true;
            scaredTimer.Start(this);
            ScaredGoal = transform.position + (shout.Direction * shout.pushForce);

            if (shout.gameObject.tag == "BearShout") {
                hp -= bearDmg;
            } else {
                hp -= animalDmg;
            }

            if (hp <= 0) {
                hp = 0;
                Destroy(gameObject);
            }
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
