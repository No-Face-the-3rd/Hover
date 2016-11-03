using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
    public float maxSpeed;
    public float turnRate;
    public float acceleration;
    public float horizDrag;

    private float horizInput = 0.0f, vertInput = 0.0f;
    private Rigidbody rb;
    private BoxCollider child;
    private float startSize, startCent;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        child = transform.FindChild("HoverField").GetComponent<BoxCollider>();
        startSize = child.size.y;
        startCent = child.center.y;
	}
	
	// Update is called once per frame
	void Update () {
        horizInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
	}

    void FixedUpdate()
    {
        if(Mathf.Abs(horizInput) > 0.0f)
        {
            rb.AddForce(new Vector3(horizInput * acceleration * Time.deltaTime, 0.0f, 0.0f), ForceMode.Force);
        }

        float angle = 180.0f - Mathf.Clamp(rb.velocity.x, -turnRate, turnRate) / turnRate * 90.0f;
        rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.Euler(new Vector3(rb.rotation.eulerAngles.x, angle, rb.rotation.eulerAngles.z)), 0.5f);

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x / Mathf.Abs(rb.velocity.x) * maxSpeed, rb.velocity.y, rb.velocity.z);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x * (1 - horizDrag), rb.velocity.y, rb.velocity.z);
        }

        if(child != null)
        {
            child.size = new Vector3(child.size.x, startSize + vertInput * 1.0f, child.size.z);
            child.center = new Vector3(child.center.x, startCent + vertInput * -0.5f, child.center.z);
        }
    }


}
