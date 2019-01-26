using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lumberjack : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector3 goal;
    public static Lumberjack Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(goal);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Tree")
        {
            goal = other.gameObject.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Tree")
        {
            goal = MainTree.Instance.transform.position;
        }
    }
}
