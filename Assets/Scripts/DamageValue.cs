using UnityEngine;
using System.Collections;

public class DamageValue : MonoBehaviour {
    public float damageValue;

    void Start()
    {
        gameObject.tag = "Harmful";
    }

    void Reset()
    {
        gameObject.tag = "Harmful";
    }

    public float getDamage()
    {
        return damageValue;
    }
}
