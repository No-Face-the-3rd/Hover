using UnityEngine;
using System.Collections;

public class HealthPickup : Pickup
{
    public float healthValue;


    void OnTriggerEnter(Collider other)
    {
        doCollisionStuff(other);
    }
    public override void doCollisionStuff(Collider other)
    {
        base.doCollisionStuff(other);
        Health healthComp = FindObjectOfType<Health>();
        healthComp.addHealth(healthValue);
    }
}
