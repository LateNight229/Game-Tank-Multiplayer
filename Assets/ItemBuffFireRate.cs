using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuffFireRate : TimeItemAvailable
{
    private float fireRate = 0.3f;
    private float randomFirate;
    protected override void Start()
    {
        base.Start();
        randomFirate = Random.Range(fireRate * 1/2, fireRate);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ControlBuffShooting tankFirate = other.GetComponent<ControlBuffShooting>();
            if (tankFirate != null)
            {
                tankFirate.ApplyBuffFireRate(randomFirate);
                Debug.Log("Buff FireRate -= " + fireRate);
                isTrigger = true;
            }
        }
    }
}
