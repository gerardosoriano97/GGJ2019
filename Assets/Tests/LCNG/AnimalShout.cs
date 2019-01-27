using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalShout : MonoBehaviour
{
    public Collider trigger;
    public Timer dieTimer;
    public Vector3 Direction { get; private set; } = Vector3.zero;
    public float pushForce = 1.0f;
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        dieTimer.Start(this);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Lumberjack")
        {
            var lumberjack = other.gameObject.GetComponent<Lumberjack>();
            lumberjack.Push(this);
            //Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public static void Emmit(GameObject parent, GameObject shoutPrefab, Vector3 direction)
    {
        var shout = Instantiate(shoutPrefab, parent.transform.position, Quaternion.identity).GetComponent<AnimalShout>();
        shout.Direction = direction;
    }
}
