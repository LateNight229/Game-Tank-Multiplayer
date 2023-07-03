using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuffDamage : TimeItemAvailable
{   
    private float damage = 50f;
    private float randomDamage;
    protected override void Start()
    {   
        base.Start();
        randomDamage = Random.Range(damage/2, damage);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ControlBuffShooting tankDamage = other.GetComponent<ControlBuffShooting>();
            if (tankDamage != null)
            {
                tankDamage.ApplyBuffDamage(randomDamage);
                Debug.Log("Buff Damage = " + randomDamage);
                isTrigger = true;
            }
        }
    }
}
