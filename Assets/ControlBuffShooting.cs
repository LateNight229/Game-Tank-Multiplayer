using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBuffShooting : MonoBehaviour
{ 
    [SerializeField] private float baseDamage = 10f;
    [SerializeField] private float maxDamage = 100f;

    [SerializeField] private float baseSpeed = 30f;
    [SerializeField] private float maxSpeed = 100f;

    [SerializeField] private float baseFireRate = 0.5f;
    [SerializeField] private float maxFireRate = 0.1f;

    [SerializeField] private float buffDuration = 10f;

    private float buffDamageTimer;
    private float buffSpeedTimer;
    private float buffFireRateTimer;

    private float buffedFireRate;
    private float buffedSpeed;
    private float buffedDamage;

    public float Speed
    {
        get
        {
            if(buffSpeedTimer > 0f)
            {
                return buffedSpeed;
            }
            else { return baseSpeed; }
        }
        set
        {
            baseSpeed = Mathf.Clamp(value, 0f, maxSpeed);
            if (buffSpeedTimer > 0f)
            {
                buffedSpeed = baseSpeed;
            }
        }
    }
    public float FireRate
    {
        get
        {
            if (buffFireRateTimer > 0f)
            {
                return buffedFireRate;
            }
            else
            {
                return baseFireRate;
            }
        }
        set
        {
            baseFireRate = Mathf.Clamp(value,  maxFireRate, baseFireRate);
            if (buffFireRateTimer > 0f)
            {
                buffedFireRate = baseFireRate;
            }
        }

    }
    public float Damage
    {
        get
        {
            if (buffDamageTimer > 0f)
            {
                return buffedDamage;
            }
            else
            {
                return baseDamage;
            }
        }
        set
        {
            baseDamage = Mathf.Clamp(value, 0f, maxDamage);
            if (buffDamageTimer > 0f)
            {
                buffedDamage = baseDamage;
            }
        }
    }

    private void Update()
    {
        if (buffDamageTimer > 0f)
        {
            buffDamageTimer -= Time.deltaTime;
            if (buffDamageTimer <= 0f)
            {
                ResetBuff(buffedDamage);
            }
        }
        if(buffSpeedTimer > 0f)
        {
            buffSpeedTimer -= Time.deltaTime;
            if (buffSpeedTimer <= 0f)
            {
                ResetBuff(buffedSpeed);
            }
        }
        if(buffFireRateTimer > 0f)
        {
            buffFireRateTimer -= Time.deltaTime;
            if(buffFireRateTimer <= 0f)
            {
                ResetBuff(buffedFireRate);
            }
        }
    }

    public void ApplyBuffDamage(float buffAmount)
    {
        buffedDamage = baseDamage + buffAmount;
        buffDamageTimer = buffDuration;
    }
    public void ApplyBuffSpeed(float buffAmount)
    {
        buffedSpeed = baseSpeed + buffAmount;
        buffSpeedTimer = buffDuration; 
    }
    public void ApplyBuffFireRate(float buffFireRate)
    {
        buffFireRate = baseFireRate - buffFireRate;
        Debug.Log("fireRate After Buff = " + buffFireRate);   
        buffFireRateTimer = buffDuration;
    }
    private void ResetBuff(float buff)
    {
        buff = 0f;
        buffDamageTimer = 0f;
    }
}
