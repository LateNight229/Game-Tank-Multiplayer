using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static CustomPropertiesKeys;
public class ButtonStartGame : MonoBehaviourPunCallbacks
{   
    TeamManager teamManager ;
   // string teamValue = TeamManager.TEAM_PROPERTY_KEY;
    string readyValue = TeamManager.STATE_READY_PROPERTY_KEY;
    public bool AreAllPlayerReady()
    {
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties[readyValue].ToString() == "False")
            {
                Debug.Log("Player: " + player.NickName + "not ready");
                return false;
            }
        }   
        return true;
    }
    public void ClickStartGame()
    {
        if (!IsRoomMaster())
        {
            Debug.Log("ban khong phai chu phong");
            return;
        }
        if (!AreAllPlayerReady())
        {
            Debug.Log("chua du nguoi chan san sang de bat dau tro choi");
            return;
        }

        photonView.RPC("LoadSceneGamePlay", RpcTarget.All);
    }

    public bool IsRoomMaster()
    {
        return PhotonNetwork.LocalPlayer.IsMasterClient;
    }

    [PunRPC]
    public void LoadSceneGamePlay()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
