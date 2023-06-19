using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{   
    public static PoolingManager Instance;
    public string colorBullet;

    private string colorTeam;    
    private PhotonView pv;
    private int ps;
    private void Awake()
    {
        Instance = this; 
        pv = GetComponent<PhotonView>();
    }
    private void Start()
    {
        if (!pv.IsMine) return;
        CreatePoolingBullet();
    }
    void CreatePoolingBullet()
    {
        GameManager.instance.CreateObj("PhotonPrefabs", "PoolStorage", "PoolingBullets", Vector3.zero, Quaternion.identity);
    }
}
