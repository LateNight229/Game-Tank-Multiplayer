using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletCtl : MonoBehaviour
{
    public GameObject bullet;
    public float maxDistance;
    [SerializeField] private float timeAvailable = 0.6f;
    [SerializeField] private float _damageAmount = 10f;
    [SerializeField] private float maxDamage = 100f;

    private float bulletTimer;
    private PhotonView HealthPv;
    private string color;
    private string namePlayer;
    private void Start()
    {   

        bulletTimer = timeAvailable; 
        int bulletLayer = gameObject.layer;
        color = LayerMask.LayerToName(bulletLayer);
    }
    public void SetDamage(string name, float damage)
    {   
        DamageAmount = damage;
        namePlayer = name;
    }
    public float DamageAmount
    {   
        get { return  _damageAmount; }
        set { _damageAmount = Mathf.Clamp(value, 0f, maxDamage); }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            SpawnExplosion(transform.position);
            Debug.Log("Damage = " + DamageAmount);
            ReturnToPool();
        }
        else if (other.CompareTag("Player")   )
        {
            int tankLayer = other.gameObject.layer;
            string tankColor = LayerMask.LayerToName(tankLayer);

            if (tankColor == color) return;

            Health tankHealth = other.GetComponent<Health>();
            HealthPv = tankHealth.photonView;
            //Debug.Log("HealthPv = " + HealthPv.Owner.ActorNumber );
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
        if (bulletTimer <= 0f) 
        {
            ReturnToPool(); SpawnExplosion(transform.position); 
        }
    }
    void ReturnToPool()
    {   
        ObjPooling.instance.ReturnBulletToPool(bullet);
        bulletTimer = timeAvailable;
    }
    [PunRPC]
    void SpawnExplosion(Vector3 positionExplosion)
    {
        ExplosionPool.Instance.SpawnExplosion(positionExplosion);
    }
}
