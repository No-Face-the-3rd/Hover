using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public GameObject follow;



    public float vertMargin, horizMargin, speed;

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
        if(follow != null)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(followT.position);
            Vector3 translation = new Vector3();
            if(screenPos.x > cam.pixelWidth - horizMargin)
            {
                translation += Vector3.right;
            }
            if(screenPos.x < horizMargin)
            {
                translation += Vector3.left;
            }
            if(screenPos.y > cam.pixelHeight - vertMargin)
            {
                translation += Vector3.up;
            }
            if(screenPos.y < vertMargin)
            {
                translation += Vector3.down;
            }
            transform.position = Vector3.Lerp(transform.position,transform.position + translation, speed * Time.deltaTime);
        }
    }
}
