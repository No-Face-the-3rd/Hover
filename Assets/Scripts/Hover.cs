using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
            Vector3 halfsies = new Vector3(transform.parent.localScale.x * cl.size.x / 2.0f, 0.0f, transform.parent.localScale.z * cl.size.z / 2.0f);
            Vector3 closePos = other.ClosestPointOnBounds(testPos);
            Quaternion orie = transform.parent.rotation;
            closePos = getClosePos(testPos,halfsies,orie * Vector3.down, range, Quaternion.identity, other, 5);
            float dist = (1.0f - (testPos.y - closePos.y) / (range));
            dist *= (strength + rb.velocity.z);

            if (rb.velocity.y < 0.0f)
                dist *= 2.0f;
            
            rb.AddForceAtPosition((rb.rotation * Vector3.up) * dist, Vector3.Scale(testPos + new Vector3(closePos.x - testPos.x,0.0f,0.0f), new Vector3(1.0f,1.0f,0.0f)), ForceMode.Acceleration );

        }
    }

    Vector3 getClosePos(Vector3 center, Vector3 halfExt, Vector3 dir, float maxRange, Quaternion orientation, Collider toCheck, int focus = 5)
    {
        Vector3 ret = new Vector3();

        float shortestDist = Mathf.Infinity;

        List<RaycastHit> hits = new List<RaycastHit>();
        int loop = ((focus % 2 == 0) ? focus / 2 : (focus - 1) / 2);

        for (int j = 0; j <= loop; j++)
        {
            for (int i = 0; i <= loop; i++)
            {
                RaycastHit[] tmp = Physics.RaycastAll(center + new Vector3(halfExt.x, 0, 0) / loop * (-loop + i) + new Vector3(0, 0, halfExt.z) / loop * (-loop + j), dir, maxRange, -1, QueryTriggerInteraction.Ignore);
                foreach (RaycastHit hit in tmp)
                {
                    hits.Add(hit);
                }
                RaycastHit[] tmp2 = Physics.RaycastAll(center - new Vector3(halfExt.x, 0, 0) / loop * (-loop + i) - new Vector3(0, 0, halfExt.z) / loop * (-loop + j), dir, maxRange, -1, QueryTriggerInteraction.Ignore);
                foreach (RaycastHit hit in tmp2)
                {
                    hits.Add(hit);
                }
                RaycastHit[] tmp3 = Physics.RaycastAll(center + new Vector3(halfExt.x, 0, 0) / loop * (-loop + i) - new Vector3(0, 0, halfExt.z) / loop * (-loop + j), dir, maxRange, -1, QueryTriggerInteraction.Ignore);
                foreach (RaycastHit hit in tmp3)
                {
                    hits.Add(hit);
                }
                RaycastHit[] tmp4 = Physics.RaycastAll(center - new Vector3(halfExt.x, 0, 0) / loop * (-loop + i) + new Vector3(0, 0, halfExt.z) / loop * (-loop + j), dir, maxRange, -1, QueryTriggerInteraction.Ignore);
                foreach (RaycastHit hit in tmp4)
                {
                    hits.Add(hit);
                }
            }
        }

        //hits = Physics.BoxCastAll(center, halfExt, dir, orientation, maxRange, -1, QueryTriggerInteraction.Ignore);
        foreach (RaycastHit hit in hits)
        {
            Debug.DrawLine(center, hit.point, Color.black, 0.05f, false);
            if(hit.collider == toCheck && hit.distance > 0.0f)
            {
                if (hit.distance < shortestDist)
                {
                    ret = hit.point;
                    shortestDist = hit.distance;

                }
                else if (Vector3.Distance(hit.point, center) < Vector3.Distance(ret, center))
                {

                    ret = hit.point;
                    shortestDist = hit.distance;
                    Debug.Log(shortestDist);
                }
            }
        }

        return ret;
    }

}
