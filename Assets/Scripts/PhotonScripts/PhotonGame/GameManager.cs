using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{   
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        CreateObj("PhotonPrefabs", "Manager", "PlayerManager", Vector3.zero, Quaternion.identity);
        CreateObj("PhotonPrefabs", "Manager", "PoolingManager", Vector3.zero, Quaternion.identity);
        CreateObj("PhotonPrefabs", "Manager", "PoolingExplosion", Vector3.zero, Quaternion.identity);
        CreateObj("PhotonPrefabs", "Manager", "PoolingDeadExplosion", Vector3.zero, Quaternion.identity);
    }
    public void CreateObj (string nameFile, string nameFile1, string namePrefab, Vector3 position, Quaternion rotation )
    {
        PhotonNetwork.Instantiate(Path.Combine(nameFile, nameFile1, namePrefab), position, rotation);
    }
   
}
