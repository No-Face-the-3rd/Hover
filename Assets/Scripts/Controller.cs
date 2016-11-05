using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
    public float maxSpeed;
    public float turnRate;
    public float acceleration;
    public float horizDrag;
    public float armDistance;

    private float horizInput = 0.0f, vertInput = 0.0f;
    private Rigidbody rb;
    private BoxCollider child;
    private float startSize, startCent;

    private Camera mainCam;

    private Rigidbody armRb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        child = transform.FindChild("HoverField").GetComponent<BoxCollider>();
        startSize = child.size.y;
        startCent = child.center.y;
        mainCam = Camera.main;
        armRb = GameObject.Find("Arm").GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        horizInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
	}

    void FixedUpdate()
    {

        doVelocity();
        doAngle();
        doVert();
        moveArm();

    }

    void doVelocity()
    {
        if(Mathf.Abs(horizInput) > 0.0f)
        {
            rb.AddForce(new Vector3(horizInput * acceleration * Time.deltaTime, 0.0f, 0.0f), ForceMode.Force);
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x / Mathf.Abs(rb.velocity.x) * maxSpeed, rb.velocity.y, rb.velocity.z);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x * (1 - horizDrag), rb.velocity.y, rb.velocity.z);
        }
    }

    void doAngle()
    {
        float angle = 180.0f - Mathf.Clamp(rb.velocity.x, -turnRate, turnRate) / turnRate * 90.0f;
        rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.Euler(new Vector3(rb.rotation.eulerAngles.x, angle, rb.rotation.eulerAngles.z)), 0.5f);
    }

    void doVert()
    {
        if(child != null)
        {
            child.size = new Vector3(child.size.x, startSize + vertInput * 1.0f, child.size.z);
            child.center = new Vector3(child.center.x, startCent + vertInput * -0.5f, child.center.z);
        }
    }

    void moveArm()
    {
        if (mainCam != null)
        {

            Vector3 mouse = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - mainCam.transform.position.z));
            Vector3 forceDir = (mouse - transform.position).normalized;

            armRb.MovePosition(rb.position + forceDir * armDistance);
        }
    }
}
