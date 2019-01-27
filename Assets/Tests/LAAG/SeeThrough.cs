using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThrough : MonoBehaviour
{

    public Transform target;
    public float area = 2.0f;

    public LayerMask layer;
    public QueryTriggerInteraction includeTriggers = QueryTriggerInteraction.Ignore;


    private List<GameObject> transparentHistory;

    // Start is called before the first frame update
    void Start()
    {
        transparentHistory = new List<GameObject>();        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            Vector3 dif = (target.position - transform.position);
            Vector3 dir = dif.normalized;

            RaycastHit[] hits;
            hits = Physics.CapsuleCastAll(transform.position, transform.position, 2.0f, dir, dif.magnitude, layer.value, includeTriggers);
            //hits = Physics.RaycastAll(transform.position, dir, dif.magnitude, layer, includeTriggers);
            foreach( RaycastHit hit in hits) {
                GameObject other = hit.collider.gameObject; 
                MeshRenderer mr = other.GetComponent<MeshRenderer>();
                if(mr == null)
                    continue;

                //Debug.Log("Hitted something rendereable");

                AutoTransparent autoTransparent = other.GetComponent<AutoTransparent>();
                if(autoTransparent != null && !autoTransparent.isTransparent) {

                    float distance = Vector3.Distance(transform.position, other.transform.position);

                    autoTransparent.EnableTransparency(distance);
                    if(!transparentHistory.Contains(other)) {
                        transparentHistory.Add(other);
                    }
                }

            }

            foreach(GameObject ct in transparentHistory) {
                bool active = false;
                foreach(RaycastHit hit in hits){
                    if(ct == hit.collider.gameObject) {
                        active = true;
                        break;
                    }
                }
                if(!active){
                    ct.GetComponent<AutoTransparent>().DisableTransparency();
                }
            }


        }
    }
}
