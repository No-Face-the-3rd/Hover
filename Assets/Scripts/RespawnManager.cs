using UnityEngine;
using System.Collections;

public class RespawnManager : MonoBehaviour {

    public Vector3 respawnLoc;
    public int lives = 5;

    private Health healthComp;
    private Energy energyComp;

    public ComponentFunc camFunction;
	
    // Use this for initialization
	void Start () {
        healthComp = GetComponent<Health>();
        energyComp = GetComponent<Energy>();

	}
	
	// Update is called once per frame
	void Update () {
        if (lives > 0)
        {

            float current = healthComp.getCurHealth();
            if (current <= 0.0f)
            {
                doRespawn();
            }
            current = energyComp.getCurEnergy();
            if (current <= 0.0f)
            {
                doRespawn();
            }
        }

	}

    public void doRespawn()
    {
        camFunction.callFunc();
        healthComp.setCurHealth(healthComp.getMaxHealth());
        energyComp.setCurEnergy(energyComp.getMaxEnergy());
        this.transform.position = respawnLoc;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        lives--;
    }


    public void setRespawnLoc(Vector3 location)
    {
        respawnLoc = location;
    }


    public int getLives()
    {
        return lives;
    }


}
