using UnityEngine;
using System.Collections;

public class Energy : MonoBehaviour {
    [SerializeField]
    private float maxEnergy;
    private float curEnergy;
    [SerializeField]
    private float drainRate;

	// Use this for initialization
	void Start () {
        curEnergy = maxEnergy;
	}
	
	// Update is called once per frame
	void Update () {
        curEnergy -= drainRate * Time.deltaTime;
        if (curEnergy < 0.0f)
            curEnergy = 0.0f;
	}

    public float getMaxEnergy()
    {
        return maxEnergy;
    }
    public float getCurEnergy()
    {
        return curEnergy;
    }
    public void setCurEnergy(float energy)
    {
        curEnergy = energy;
    }
    public void addEnergy(float addition)
    {
        curEnergy += addition;
    }

    public float getPercentEnergy()
    {
        return curEnergy / maxEnergy;
    }
}
