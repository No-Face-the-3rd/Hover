using UnityEngine;
using System.Collections;

public class EnergyPickup : Pickup {
    public float energyValue;

    void OnTriggerEnter(Collider other)
    {
        doCollisionStuff(other);
    }

    public override void doCollisionStuff(Collider other)
    {
        base.doCollisionStuff(other);
        Energy energyComp = FindObjectOfType<Energy>();
        energyComp.addEnergy(energyValue);
    }
}
