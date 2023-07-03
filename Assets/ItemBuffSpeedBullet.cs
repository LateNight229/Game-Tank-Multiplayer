using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuffSpeedBullet : TimeItemAvailable
{
    private float speed = 20f;
    protected override void Start()
    {
        base.Start();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ControlBuffShooting tankBuffSpeedbullet = other.GetComponent<ControlBuffShooting>();
            if (tankBuffSpeedbullet != null)
            {
                tankBuffSpeedbullet.ApplyBuffSpeed(speed);
                Debug.Log("speed += " + speed);
                isTrigger = true;
            }
        }
    }

}
