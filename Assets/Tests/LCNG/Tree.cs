using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public static List<Tree> Items;
    private void Awake()
    {
        Items.Add(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Tree")
        {

        }
        else
        {

        }
    }

    private void OnDestroy()
    {
        Items.Remove(this);
    }
}
