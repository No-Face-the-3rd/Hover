using UnityEngine;
using System.Collections;

public class ReturnToUp : MonoBehaviour {

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
        if(rb != null)
        {
            if (rb.rotation.x != 0.0f && rb.angularVelocity.x < 0.1f)
                rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.Euler(new Vector3(0.0f, rb.rotation.eulerAngles.y, rb.rotation.eulerAngles.z)),0.1f);
            if (rb.rotation.z != 0.0f && rb.angularVelocity.z < 0.1f)
                rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.Euler(new Vector3(rb.rotation.eulerAngles.x, rb.rotation.eulerAngles.y, 0.0f)), 0.1f);

        }
	}
}
