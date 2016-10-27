using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
    public float maxSpeed;
    public float turnRate;
    public float acceleration;

    private float horizInput = 0.0f, vertInput = 0.0f;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
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

        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector3(maxSpeed, rb.velocity.y, rb.velocity.z);
        }
    }


}
