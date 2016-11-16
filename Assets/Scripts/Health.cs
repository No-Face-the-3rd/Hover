using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    [SerializeField]
    private float maxHealth;
    private float curHealth;


	// Use this for initialization
	void Start () {
        curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (curHealth < 0.0f)
            curHealth = 0.0f;
	}

    public float getMaxHealth()
    {
        return maxHealth;
    }
    public float getCurHealth()
    {
        return curHealth;
    }
    public void setCurHealth(float health)
    {
        curHealth = health;
    }
    public void addHealth(float addition)
    {
        curHealth += addition;
    }

    public float getPercentHealth()
    {
        return curHealth / maxHealth;
    }
}
