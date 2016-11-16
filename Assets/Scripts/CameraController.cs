using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public GameObject follow;



    public float vertMargin, horizMargin, speed;

    private bool fol = false;

    private Transform followT;
    private Camera cam;
	// Use this for initialization
	void Start () {
        followT = follow.GetComponent<Transform>();
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if(fol)
        {
            retainTarget();
        }
        else
        {
            centerCam();
        }

        Debug.Log(fol);
    }

    private void retainTarget()
    {
        if (follow != null)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(followT.position);
            Vector3 translation = new Vector3();
            if (screenPos.x > cam.pixelWidth - horizMargin)
            {
                translation += Vector3.right;
            }
            if (screenPos.x < horizMargin)
            {
                translation += Vector3.left;
            }
            if (screenPos.y > cam.pixelHeight - vertMargin)
            {
                translation += Vector3.up;
            }
            if (screenPos.y < vertMargin)
            {
                translation += Vector3.down;
            }
            transform.position = Vector3.Lerp(transform.position, transform.position + translation, speed * Time.deltaTime);
        }
    }

    public void centerCam()
    {
        fol = false;
        transform.position = Vector3.Lerp(transform.position, new Vector3(followT.position.x, followT.position.y, transform.position.z),speed * Time.deltaTime);
        if (Mathf.Abs(transform.position.x - followT.position.x) < 0.1f && Mathf.Abs(transform.position.y - followT.position.y) < 0.1f)
            fol = true;
    }
}
