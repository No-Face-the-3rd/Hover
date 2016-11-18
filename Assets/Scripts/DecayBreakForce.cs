using UnityEngine;
using System.Collections;

public class DecayBreakForce : MonoBehaviour {
    public float target;
    public float time;

    private float curTime = 0.0f;
    public SpringJoint spring;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(curTime < time && spring != null)
        {
            curTime += Time.deltaTime;
            spring.breakForce = Mathf.Lerp(spring.breakForce, target, curTime / time);
        }
	}
}
