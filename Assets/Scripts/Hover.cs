using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hover : MonoBehaviour {

    public float strength = 0.0f;


    private Rigidbody rb;
    private BoxCollider cl;
    private Collider pCl;

    private RaycastHit[] hits;
    private bool rayDown = false;

    class rayControl
    {
        public Vector3 center;
        public Vector3 halfExt;
        public Vector3 dir;
        public float maxRange;
        public Quaternion orientation;
        public int focus;

        public rayControl()
        {
            center = Vector3.zero;
            halfExt = Vector3.one;
            dir = Vector3.forward;
            maxRange = 1.0f;
            orientation = Quaternion.identity;
            focus = 5;
        }

        public rayControl(Vector3 pCenter, Vector3 pHalfExt, Vector3 pDir, float pMaxRange, Quaternion pOrientation, int pFocus = 5)
        {
            center = pCenter;
            halfExt = pHalfExt;
            dir = pDir;
            maxRange = pMaxRange;
            orientation = pOrientation;
            focus = pFocus;
        }
    }

    // Use this for initialization
    void Start () {
	    if(transform.parent != null)
        {
            rb = transform.parent.gameObject.GetComponent<Rigidbody>();
        }
        cl = GetComponent<BoxCollider>();
        pCl = rb.gameObject.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if(rayDown)
        {
            rayControl pos = getPosInfo();
            hits = getHits(pos);
            doForce(pos);
        }

	}

    void OnTriggerStay(Collider other)
    {
        if (rb != null)
        {
            rayDown = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if(rb != null)
        {
            rayDown = false;
        }
    }

    RaycastHit[] getHits(Vector3 center, Vector3 halfExt, Vector3 dir, float maxRange, Quaternion orientation, int focus = 5)
    {
        List<RaycastHit> hits = new List<RaycastHit>();
        int loop = ((focus % 2 == 0) ? focus / 2 : (focus - 1) / 2);

        int mask = ~(1 << 2 | 1 << 8);

        for (int j = 0; j <= loop; j++)
        {
            for (int i = 0; i <= loop; i++)
            {
                Vector3 offset = orientation * (new Vector3(halfExt.x, 0, 0) / loop * (-loop + i) + new Vector3(0, 0, halfExt.z) / loop * (-loop + j));
                Debug.DrawLine(center + offset, center, Color.cyan, 0.02f, false);
                RaycastHit tmp;
                if(Physics.Raycast(center + offset, dir, out tmp, maxRange, mask, QueryTriggerInteraction.Ignore))
                {
                    hits.Add(tmp);
                }

                offset = orientation * (-new Vector3(halfExt.x, 0, 0) / loop * (-loop + i) - new Vector3(0, 0, halfExt.z) / loop * (-loop + j));
                Debug.DrawLine(center + offset, center, Color.cyan, 0.02f, false);
                if(Physics.Raycast(center + offset, dir, out tmp, maxRange, mask, QueryTriggerInteraction.Ignore))
                {
                    hits.Add(tmp);
                }
                offset = orientation * (new Vector3(halfExt.x, 0, 0) / loop * (-loop + i) - new Vector3(0, 0, halfExt.z) / loop * (-loop + j));
                Debug.DrawLine(center + offset, center, Color.cyan, 0.02f, false);
                if(Physics.Raycast(center + offset, dir, out tmp, maxRange, mask, QueryTriggerInteraction.Ignore))
                {
                    hits.Add(tmp);
                }
                offset = orientation * (-new Vector3(halfExt.x, 0, 0) / loop * (-loop + i) + new Vector3(0, 0, halfExt.z) / loop * (-loop + j));
                Debug.DrawLine(center + offset, center, Color.cyan, 0.02f, false);
                if(Physics.Raycast(center + offset, dir, out tmp, maxRange, mask, QueryTriggerInteraction.Ignore))
                {
                    hits.Add(tmp);
                }
            }
        }

        return hits.ToArray();
    }

    RaycastHit[] getHits(rayControl control)
    {
        RaycastHit[] hits = getHits(control.center, control.halfExt, control.dir, control.maxRange, control.orientation, control.focus);
        return hits;
    }

    rayControl getPosInfo()
    {
        float range = cl.size.y * rb.transform.localScale.y;
        Quaternion orie = rb.rotation;
        Vector3 testPos = rb.position + orie * (cl.center + new Vector3(0.0f, range * 0.5f, 0.0f));
        Vector3 halfsies = new Vector3(transform.parent.localScale.x * cl.size.x / 2.0f, 0.0f, transform.parent.localScale.z * cl.size.z / 2.0f);
        rayControl ret = new rayControl(testPos, halfsies, Vector3.down, range, orie, 5);
        return ret;
    }

    void doForce(rayControl control)
    {
        foreach (RaycastHit hit in hits)
        {
            float dist = (1.0f - (hit.distance) / (control.maxRange));
            dist *= (strength + Mathf.Abs(rb.velocity.x) / (float.MaxValue / 100.0f));

            Rigidbody other = hit.collider.GetComponent<Rigidbody>();

            if (rb.velocity.y < 0.0f)
                dist *= 2.0f;

            Vector3 forcePos = hit.point + Vector3.up * hit.distance;
            Debug.DrawLine(hit.point + Vector3.up * hit.distance, hit.point, Color.black, 0.02f, false);

            rb.AddForceAtPosition((rb.rotation * Vector3.up) * dist, forcePos, ForceMode.Acceleration);

            if(other != null)
            {
                other.AddForceAtPosition(Vector3.down * dist, hit.point, ForceMode.Acceleration);
            }
            Debug.DrawLine(forcePos, forcePos + (rb.rotation * Vector3.up) * dist, Color.green, 0.02f, false);
        }
    }

}
