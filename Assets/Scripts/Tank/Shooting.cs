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
    public GameObject firePoint;

    [SerializeField] private float _bulletSpeed = 30f;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private float _damageAmount = 10f;

    public string team;
    private PhotonView pv;
    private int poTeamF;
    private float fireTimer;
    private bool color;
    private string nameplayer;
    void Start()
    {   
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine) return;
        CheckColorMyBullet();
        nameplayer = pv.Owner.NickName; 
    }
    void CheckColorMyBullet()
    {
        UpdatePropertiesPlayer.Instance.GetColorAndPositionTankLocal(ref team, ref poTeamF);
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
            GetDamageByName();
            float afterSpeed = _bulletSpeed;
            float afterDamage = _damageAmount;
            Vector3 bulletDirection = firePoint.transform.forward;
            pv.RPC("ShootBullet", RpcTarget.All,afterSpeed, afterDamage, bulletDirection, firePoint.transform.position,firePoint.transform.rotation, color);
            fireTimer = _fireRate;

        }
        if(fireTimer > 0f) fireTimer -=Time.deltaTime;
    }
    [PunRPC]
    public void ShootBullet(float speed,float damage,Vector3 bulletDirection, Vector3 position, Quaternion rotation, bool color)
    {
        GameObject bullet = ObjPooling.instance.GetBullet(position, rotation, color);
        BulletCtl bulletCtl = bullet.GetComponent<BulletCtl>();
        bulletCtl.SetDamage(nameplayer, damage);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = bulletDirection * speed;

    }
    private void GetDamageByName()
    {
        string nameOwner = pv.Owner.NickName;
        GameObject OwnerTank = UpdatePropertiesPlayer.Instance.GetplayerObjByName(nameOwner);
        if(OwnerTank != null)
        {
            ControlBuffShooting ctrlBullet = OwnerTank.GetComponent<ControlBuffShooting>();
            _damageAmount = ctrlBullet.Damage;
            _fireRate = ctrlBullet.FireRate;
            _bulletSpeed = ctrlBullet.Speed;
        }
        else
        {
            Debug.Log("OwnerTank nullllllllllll");
        }
    }
}
