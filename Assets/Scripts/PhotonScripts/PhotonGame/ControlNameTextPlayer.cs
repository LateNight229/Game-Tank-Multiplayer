using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlNameTextPlayer : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI textNamePlayer;

    private Camera mainCamera;
    private string namePlayerUpdate = "null";
    private PhotonView pv;

    private void Awake()
    {
        //mainCamera = playerStart.Instance.GetCamera();
        mainCamera = Camera.main;
    }
    private void Start()
    {   
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
            UpdateNamePlayer();
        }
    }

    private void Update()
    { 
        Vector3 cameraDirection = mainCamera.transform.position - transform.position;
        cameraDirection.z = 0;
        cameraDirection.x = -1;
        Quaternion lookRotation = Quaternion.LookRotation(cameraDirection, Vector3.up);
        transform.rotation = lookRotation;
    }
    void UpdateNamePlayer()
    {
        UpdatePropertiesPlayer.Instance.GetNamePlayer(ref namePlayerUpdate);
        textNamePlayer.text = namePlayerUpdate;
        pv.RPC("SynNamePlayer", RpcTarget.Others,namePlayerUpdate);
    }
    [PunRPC]
    void SynNamePlayer(string namePlayer)
    {
        textNamePlayer.text = namePlayer;
    }
}
