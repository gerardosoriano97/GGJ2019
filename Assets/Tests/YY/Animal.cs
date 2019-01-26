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

    public GaiaSpecie specie;
    // Start is called before the first frame update
    void Start()
    {
        BearCry.Instance.gaiaCry.AddListener(GaiaAttend);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy() {
        BearCry.Instance.gaiaCry.RemoveListener(GaiaAttend);
    }
    void GaiaAttend(int specieIndex) {
        if ((int)specie == specieIndex) {
            agent.SetDestination(BearCry.Instance.transform.position);
        }
    }
}
