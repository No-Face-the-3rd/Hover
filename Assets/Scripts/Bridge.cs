using UnityEngine;
using System.Collections;

public class Bridge : MonoBehaviour {

    public Vector3 leftAnchor, rightAnchor;
    public GameObject BridgePart;

    public void Start()
    {
        buildBridge();
    }

    public void Reset()
    {
        leftAnchor = Vector3.left + Vector3.up;
        rightAnchor = Vector3.right + Vector3.up;
    }

    public void buildBridge()
    {
        Transform parent = transform;

        foreach(Transform child in parent)
        {
            Destroy(child.gameObject);
        }
        int leftPanels = ((int)(leftAnchor).magnitude) - 1;
        int rightPanels = ((int)(rightAnchor).magnitude) - 1;

        GameObject leftCur = null;
        GameObject rightCur = null;
        Vector3 leftAnc = leftAnchor + transform.position;
        Vector3 rightAnc = rightAnchor + transform.position;


        for (int i = 0; i < leftPanels; i++)
        {
            GameObject tmp = (GameObject)GameObject.Instantiate(BridgePart,leftAnc + -leftAnchor.normalized * leftAnchor.magnitude / (leftPanels + 2),Quaternion.identity);
            tmp.transform.parent = transform;
            SpringJoint spring = tmp.AddComponent<SpringJoint>();
            tmp.GetComponent<DecayBreakForce>().spring = spring;
            spring.autoConfigureConnectedAnchor = false;
            if(leftCur == null)
            {
                spring.connectedAnchor = leftAnc;
            }
            else
            {
                spring.connectedBody = leftCur.GetComponent<Rigidbody>();
                spring.connectedAnchor = Vector3.right * 0.5f;
            }
            spring.breakForce = 1000000.0f;
            spring.anchor = Vector3.left * 0.5f;
            spring.tolerance = 0.1f;
            spring.spring = 1000.0f;
            leftAnc = tmp.transform.position + Vector3.right * 0.5f;
            leftCur = tmp;
        }

        for (int i = 0; i < rightPanels; i++)
        {
            GameObject tmp = (GameObject)GameObject.Instantiate(BridgePart, rightAnc + -rightAnchor.normalized * rightAnchor.magnitude / (rightPanels + 2), Quaternion.identity);
            tmp.transform.parent = transform;
            SpringJoint spring = tmp.AddComponent<SpringJoint>();
            tmp.GetComponent<DecayBreakForce>().spring = spring;
            spring.autoConfigureConnectedAnchor = false;
            if (rightCur == null)
            {
                spring.connectedAnchor = rightAnc;
            }
            else
            {
                spring.connectedBody = rightCur.GetComponent<Rigidbody>();
                spring.connectedAnchor = Vector3.left * 0.5f;
            }
            spring.breakForce = 1000000.0f;
            spring.anchor = Vector3.right * 0.5f;
            spring.tolerance = 0.1f;
            spring.spring = 1000.0f;
            rightAnc = tmp.transform.position + Vector3.left * 0.5f;
            rightCur = tmp;
        }
        GameObject tmpMid = null;
        SpringJoint springLeft = null;
        if((leftCur.transform.position - rightCur.transform.position).magnitude > 2.25f)
        {
            tmpMid = (GameObject)GameObject.Instantiate(BridgePart, leftAnc + -leftAnchor.normalized * leftAnchor.magnitude / (leftPanels + 1), Quaternion.identity);
            springLeft = tmpMid.AddComponent<SpringJoint>();
            springLeft.autoConfigureConnectedAnchor = false;
            springLeft.connectedBody = leftCur.GetComponent<Rigidbody>();
            springLeft.connectedAnchor = Vector3.left * 0.5f;
            springLeft.breakForce = 1000000.0f;
            springLeft.anchor = Vector3.left * 0.5f;
            springLeft.tolerance = 0.1f;
            springLeft.spring = 1000.0f;
        }
        else
        {
            tmpMid = leftCur;
            springLeft = leftCur.GetComponent<SpringJoint>();
        }
        tmpMid.transform.parent = transform;
        tmpMid.GetComponent<DecayBreakForce>().spring = springLeft;
        SpringJoint springRight = tmpMid.AddComponent<SpringJoint>();
        tmpMid.AddComponent<DecayBreakForce>().spring = springRight;
        springRight.autoConfigureConnectedAnchor = false;

        springRight.connectedBody = rightCur.GetComponent<Rigidbody>();

        springRight.connectedAnchor = Vector3.left * 0.5f;
        springRight.breakForce = 1000000.0f;
        springRight.anchor = Vector3.right * 0.5f;
        springRight.tolerance = 0.1f;
        springRight.spring = 1000.0f;

    }

}
