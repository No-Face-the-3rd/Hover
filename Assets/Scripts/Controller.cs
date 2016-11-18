using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
    public float maxSpeed;
    public float turnRate;
    public float acceleration;
    public float horizDrag;
    public float armDistance;

    public GameObject primary;
    public float primaryCost;

    public ComponentFunc getEnergy;

    private float horizInput = 0.0f, vertInput = 0.0f;
    private Rigidbody rb;
    private BoxCollider child;
    private float startSize, startCent;

    private Camera mainCam;

    private Rigidbody armRb;
    private Vector3 armDir;

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
        if(Input.GetMouseButtonDown(0))
        {
            firePrim();
        }
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
            armDir = forceDir;
            armRb.MovePosition(rb.position + forceDir * armDistance);
        }
    }

    void firePrim()
    {
        if((float)getEnergy.callFunc() > primaryCost)
        {
            GameObject bullet = (GameObject)GameObject.Instantiate(primary, armRb.position, Quaternion.LookRotation(armDir));
            GetComponent<Energy>().addEnergy(-primaryCost);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Harmful")
        {
            float damage = collision.gameObject.GetComponent<DamageValue>().getDamage();
            Health healthComp = GetComponent<Health>();
            healthComp.addHealth(-damage);
        }
    }
}
