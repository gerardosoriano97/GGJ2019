using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerTest : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector3 goal;
    public static PlayerControllerTest Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        goal = MainTree.Instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(goal);
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("acaacsd");
        if (collision.gameObject.tag == "Tree")
        {
            goal = collision.gameObject.transform.position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            goal = MainTree.Instance.transform.position;
        }
    }
}
