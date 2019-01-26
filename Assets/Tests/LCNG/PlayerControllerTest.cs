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
        Debug.Log(MainTree.Instance);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(goal);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("acaacsd");
        if (other.gameObject.tag == "Tree")
        {
            goal = other.gameObject.transform.position;
        }
    }

    private void OnTriggerStay(Collision collision)
    {
    }

    private void OnCollisionExit(Collision collision)
    {
}
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Tree")
        {
            goal = MainTree.Instance.transform.position;
        }
    }
}
