using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {

    public float strength = 0.0f;


    private Rigidbody rb;
    private BoxCollider cl;
	// Use this for initialization
	void Start () {
	    if(transform.parent != null)
        {
            rb = transform.parent.gameObject.GetComponent<Rigidbody>();
        }
        cl = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if (rb != null)
        {
            float range = cl.size.y * rb.transform.localScale.y;
            Vector3 testPos = transform.parent.position + cl.center + new Vector3(0.0f, range * 0.5f, 0.0f);
            Vector3 closePos = other.ClosestPointOnBounds(testPos);
            float dist = (1.0f - (testPos.y - closePos.y) / (range)) ;
            dist *= (strength + rb.velocity.z);

            if (rb.velocity.y < 0.0f)
                dist *= 2.0f;
            
            rb.AddForceAtPosition((rb.rotation * Vector3.up) * dist, Vector3.Scale(testPos + new Vector3(closePos.x - testPos.x,0.0f,0.0f), new Vector3(1.0f,1.0f,0.0f)), ForceMode.Acceleration );

        }
    }

}
