using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RevivalPlayer : MonoBehaviourPunCallbacks
{   
    //public static RevivalPlayer instance;

    [SerializeField] private float timeRevival = 3f;
    GameObject tankObj;
    GameObject turretObj;
    public UnityEvent<CountdownRevival> StartCD;

    private int positionIndex;
    private Transform positionSpawn;

    private void Awake()
    {
        //instance = this;
    }
    private void Start()
    {
       
    }
    public void ResetPlayerPosition(int OwnerActorNumber , string color)
    {
        RevivalPlayer revival = Instantiate(this);
        //Debug.Log("OwnerActorNumver = " + OwnerActorNumber);
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if (player.ActorNumber == OwnerActorNumber)
            {   
                tankObj = UpdatePropertiesPlayer.Instance.GetPlayerObj(player);
               // Debug.Log("TankObj = " + tankObj.name);
                turretObj = UpdatePropertiesPlayer.Instance.GetTurretObj(player);
                if(tankObj != null && turretObj != null)
                {
                    SetActivePlayer(false);
                    RePositionPlayer(OwnerActorNumber, color);
                    Debug.Log("Start Invoke !");
                    Invoke("RevivePlayer", 2f);
                    
                }
            }
        }
    }
    private void SetActivePlayer(bool active)
    {
        if (active == true)
        {
            tankObj.SetActive(true);
            turretObj.SetActive(true);
        }
        else
        {
            tankObj.SetActive(false);
            turretObj.SetActive(false);

        }
    }
    private void RevivePlayer()
    {
        SetActivePlayer(true);
        ListRevivalPlayer.Instance.AddActive(this);
    }

    private void RePositionPlayer(int OwnerActorNumber, string color)
    {
        UpdatePropertiesPlayer.Instance.GetPositionSpawn(ref positionIndex, OwnerActorNumber);
        PlayerManager.instance.RepositionPlayer(positionIndex,ref positionSpawn, color);
        tankObj.transform.position = positionSpawn.position;
        tankObj.transform.rotation = positionSpawn.rotation;
        turretObj.transform.position =positionSpawn.position;
    }
  
}
