using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePropertiesPlayer : Singleton<UpdatePropertiesPlayer>
{

    private const string teamValue = TeamManager.TEAM_PROPERTY_KEY;
    private const string countIndex = TeamManager.COUNT_POSITION_START;
    private const string namePlayer = LobbyManager.NAME_PLAYER;

    public Tuple<string,int> GetColorAndPositionTank(ref string colorTeam , ref int positionIndex)
    {
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if (player.IsLocal)
            {
                colorTeam = player.CustomProperties[teamValue].ToString() == "team Blue" ? "blue" : "red";
               string spawnIndex = player.CustomProperties[countIndex].ToString();
                positionIndex = int.Parse(spawnIndex);
            }
        }
        return new Tuple<string,int>(colorTeam, positionIndex);
    }
    public String GetColor(ref string colorTeam, int OwnerActorNumber)
    {
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if (player.ActorNumber == OwnerActorNumber)
            {
                colorTeam = player.CustomProperties[teamValue].ToString() == "team Blue" ? "blue" : "red";
            }
        }
        return colorTeam;
    }
    public string GetNamePlayer(ref string NamePlayer)
    {   
        foreach(Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if (player.IsLocal)
            {   
                NamePlayer = player.CustomProperties[namePlayer].ToString();

                return NamePlayer;
            }
        }
        return null;
    }
    public GameObject GetPlayerObj(Photon.Realtime.Player player)
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            PhotonView photonView = playerObject.GetComponent<PhotonView>();
            if (photonView != null && photonView.Owner == player)
            {
                return playerObject;
            }
        }
        return null;
    }
    public GameObject GetTurretObj(Photon.Realtime.Player player)
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Turret");
        foreach (GameObject playerObject in playerObjects)
        {
            PhotonView photonView = playerObject.GetComponent<PhotonView>();
            if (photonView != null && photonView.Owner == player)
            {
                return playerObject;
            }
        }
        return null;
    }
    public int GetPositionSpawn(ref int positionIndex, int OwnerActorNumber)
    {
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if (player.ActorNumber == OwnerActorNumber)
            {
                string spawnIndex = player.CustomProperties[countIndex].ToString();
                positionIndex = int.Parse(spawnIndex);
            }
        }
        return positionIndex;

    }

}
