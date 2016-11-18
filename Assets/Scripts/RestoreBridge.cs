using UnityEngine;
using System.Collections;

public class RestoreBridge : MonoBehaviour {

    public float restoreTime;
    private float curTime = 0.0f;

    private bool shouldRestore = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(shouldRestore)
        {
            curTime += Time.deltaTime;
        }
        if(curTime >= restoreTime)
        {
            transform.parent.GetComponent<Bridge>().buildBridge();
        }
	}

    void OnJointBreak(float breakForce)
    {
        shouldRestore = true;
    }
}
