using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class RevivalPlayer : MonoBehaviourPunCallbacks
{   
    public static RevivalPlayer instance;

    [SerializeField] private float timeRevival = 3f;
    GameObject tankObj;
    GameObject turretObj;

    private int positionIndex;
    private Transform positionSpawn; 
    private bool isReviving = false;
    private float revivalTimer = 0f;
    private void Awake()
    {
        instance = this;
    }
    public void ResetPlayerPosition(int OwnerActorNumber , string color)
    {   
        Debug.Log("OwnerActorNumver = " + OwnerActorNumber);
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if (player.ActorNumber == OwnerActorNumber)
            {   
                tankObj = UpdatePropertiesPlayer.Instance.GetPlayerObj(player);
                Debug.Log("TankObj = " + tankObj.name);
                turretObj = UpdatePropertiesPlayer.Instance.GetTurretObj(player);
                if(tankObj != null && turretObj != null)
                {
                    //SetActivePlayer(false);
                    DisableTank();
                    //RevivePlayer();
                    //Invoke("RevivePlayer", timeRevival);
                    //StartCoroutine(WaitAndRevival(timeRevival, OwnerActorNumber,color));
                    RePositionPlayer(OwnerActorNumber, color);
                }
            }
        }
    }
    private IEnumerator WaitAndRevival(float time,int OwnerActorNumber,string color)
    {
        yield return new WaitForSeconds(time);
        RePositionPlayer(OwnerActorNumber, color);
    }
    private void DisableTank()
    {
        tankObj.transform.position = new Vector3(tankObj.transform.position.x, -100f, tankObj.transform.position.z);
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
