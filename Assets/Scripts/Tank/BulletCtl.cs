﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletCtl : MonoBehaviour
{
    public GameObject bullet;
    public float maxDistance;
    [SerializeField] public float bulletRate = 1f;
    [SerializeField] private float DamageAmount = 50f;

    private float bulletTimer;
    private PhotonView HealthPv;
    private string color;
    private void Start()
    {
        bulletTimer = bulletRate; 
        int bulletLayer = gameObject.layer;
        color = LayerMask.LayerToName(bulletLayer);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            SpawnExplosion(transform.position);
            ReturnToPool();
        }
        else if (other.CompareTag("Player")   )
        {
            int tankLayer = other.gameObject.layer;
            string tankColor = LayerMask.LayerToName(tankLayer);

            if (tankColor == color) return;

            Health tankHealth = other.GetComponent<Health>();
            HealthPv = tankHealth.photonView;
            Debug.Log("HealthPv = " + HealthPv.Owner.ActorNumber );
            if (tankHealth != null )
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    HealthPv.RPC("MasterClientTakeDamage", RpcTarget.MasterClient, DamageAmount, HealthPv.ViewID);

                }
                SpawnExplosion(transform.position);
                ReturnToPool();
            }
        }

    }
    private void  Update()
    {
        bulletTimer -= Time.deltaTime;
        if(bulletTimer <= 0f) ReturnToPool();
    }
    void ReturnToPool()
    {   
        ObjPooling.instance.ReturnBulletToPool(bullet);
        bulletTimer = bulletRate;
    }
    [PunRPC]
    void SpawnExplosion(Vector3 positionExplosion)
    {
        ExplosionPool.Instance.SpawnExplosion(positionExplosion);
    }
}
