using UnityEngine;
using System.Collections;

public class DirectionalMove : MonoBehaviour {
    public float speed;

    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
