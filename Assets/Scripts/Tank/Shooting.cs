using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shooting : MonoBehaviourPunCallbacks
{   
    public static Shooting instance;
    private void Awake()
    {
        instance = this; 
    }

    [SerializeField] private float bulletSpeed = 30f;
    /*[SerializeField]*/ private float fireRate = 0.1f;
    public GameObject firePoint;

    private PhotonView pv;
    private int poTeamF;
    private float fireTimer;
    private bool color;
    public string team;
    void Start()
    {   
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine) return;
        CheckColorMyBullet();
    }
    void CheckColorMyBullet()
    {
        UpdatePropertiesPlayer.Instance.GetColorAndPositionTank(ref team, ref poTeamF);
        if (team == "blue")
        {
            color = true;
        }
        else color = false;
    }
    void Update()
    {
        if (!pv.IsMine) { return; }
        if (Input.GetMouseButtonDown(0) && fireTimer <= 0f)
        {
            Vector3 bulletDirection = firePoint.transform.forward;
            pv.RPC("ShootBullet", RpcTarget.All,bulletDirection, firePoint.transform.position,firePoint.transform.rotation, color);
            fireTimer = fireRate;
        }
        if(fireTimer > 0f) fireTimer -=Time.deltaTime;
    }
    [PunRPC]
    public void ShootBullet(Vector3 bulletDirection, Vector3 position, Quaternion rotation, bool color)
    {
        GameObject bullet = ObjPooling.instance.GetBullet(position, rotation, color);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = bulletDirection * bulletSpeed;

    }
}
