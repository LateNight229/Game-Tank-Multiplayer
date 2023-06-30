using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ExitGames.Client.Photon;
//using static CustomPropertiesKeys;

public class TeamManager : MonoBehaviourPunCallbacks
{
    public static TeamManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        ClickReady();
    }
    public const string TEAM_PROPERTY_KEY = "team";
    public const string STATE_READY_PROPERTY_KEY = "state_ready";
    public const string COUNT_POSITION_START = "positon_Count";

    private const string TEAM_BLUE_VALUE = "team Blue";
    private const string TEAM_RED_VALUE = "team Red";
    private const bool STATE_IS_READY_VALUE = true;
    private const bool STATE_IS_NOT_READY_VALUE = false;
    private const int MAX_PLAYER_TEAM = 3;

    private List<string> teamBlue = new List<string>();
    private List<string> teamRed = new List<string>();
    private bool canSwitch = false;
    private bool isReady = false;
    private int blueCount = 0;
    private int redCount = 0;
    private int myCount = 0;
    
    ExitGames.Client.Photon.Hashtable Props = new ExitGames.Client.Photon.Hashtable();
    ExitGames.Client.Photon.Hashtable ReadyState = new ExitGames.Client.Photon.Hashtable();
    ExitGames.Client.Photon.Hashtable PostionStart = new ExitGames.Client.Photon.Hashtable();
    public void UpdatePlayerAvailable()
    {
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            Debug.Log("Player ID: " + player.ActorNumber + " | Player Name: " + player.NickName);
            if (!player.IsLocal)
            {
                ExitGames.Client.Photon.Hashtable props = player.CustomProperties;
                Debug.Log("Props: " + props[TEAM_PROPERTY_KEY]);
                string teamValue = props[TEAM_PROPERTY_KEY].ToString();

                if (teamValue == TEAM_BLUE_VALUE)
                {
                    teamBlue.Add(player.NickName);
                    blueCount++;
                    //Debug.Log("Blue count: " + blueCount);
                    UpdateUiLocal();
                    photonView.RPC("UpdateUiAll", RpcTarget.All);
                }
                else if (teamValue == TEAM_RED_VALUE)
                {
                    teamRed.Add(player.NickName);
                    redCount++;
                    //Debug.Log("red count: " + redCount);
                    UpdateUiLocal();
                    photonView.RPC("UpdateUiAll", RpcTarget.All);
                }

            }

        }

    }
    public void NotifiJoinRoom()
    {
        string teamToAddTo = teamBlue.Count <= teamRed.Count ? TEAM_BLUE_VALUE : TEAM_RED_VALUE;
        Props[TEAM_PROPERTY_KEY] = teamToAddTo.ToString();
        ReadyState[STATE_READY_PROPERTY_KEY] = STATE_IS_NOT_READY_VALUE.ToString();
        PhotonNetwork.SetPlayerCustomProperties(Props);
        PhotonNetwork.SetPlayerCustomProperties(ReadyState);
        SavePosition(teamToAddTo);
        photonView.RPC("AddPlayerTeam", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, Props[TEAM_PROPERTY_KEY].ToString());
        photonView.RPC("UpdateUiAll", RpcTarget.All);
       
    }
    public void SavePosition(string team)
    {   if (team == TEAM_BLUE_VALUE)
            myCount = blueCount + 1;
        else { myCount = redCount + 1; }
       // Debug.Log("My count: " + myCount);
        PostionStart[COUNT_POSITION_START] = myCount.ToString();
        PhotonNetwork.SetPlayerCustomProperties(PostionStart);
        //Debug.Log("Property my count: " + PostionStart[COUNT_POSITION_START].ToString());
    }
    public void ClickSwitchTeam()
    {
        string currentTeam = (string)PhotonNetwork.LocalPlayer.CustomProperties[TEAM_PROPERTY_KEY];
        string newTeam = currentTeam == TEAM_BLUE_VALUE ? TEAM_RED_VALUE : TEAM_BLUE_VALUE;
       
        if(CheckSlotTeam(newTeam) && CheckUnReady() )
        {
            Props[TEAM_PROPERTY_KEY] = newTeam.ToString();
            PhotonNetwork.LocalPlayer.SetCustomProperties(Props);
            Debug.Log("now Team: " + Props[TEAM_PROPERTY_KEY]);
            photonView.RPC("UpdateSwitchTeam", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, newTeam);
            photonView.RPC("UpdateUiAll", RpcTarget.All);
            canSwitch = false;
        }
        return;
    }
    public bool CheckSlotTeam( string newTeam)
    {
        if (newTeam == TEAM_BLUE_VALUE && teamBlue.Count < MAX_PLAYER_TEAM)
        {
           return canSwitch = true;
            
        }
        else if (newTeam == TEAM_RED_VALUE && teamRed.Count < MAX_PLAYER_TEAM)
        {
           return canSwitch = true;
        } 
        return canSwitch = false;
    }
    public bool CheckUnReady()
    {
        if (isReady == true) return false;
        return true;
    }
    public void ClickReady()
    {   
        if(isReady == false)
        {
            ReadyState[STATE_READY_PROPERTY_KEY] = STATE_IS_READY_VALUE;
            isReady = true;
        }
        else
        {
            ReadyState[STATE_READY_PROPERTY_KEY] = STATE_IS_NOT_READY_VALUE;
            isReady = false;
        } 
        PhotonNetwork.LocalPlayer.SetCustomProperties(ReadyState); 
        bool currentReady = (bool)ReadyState[STATE_READY_PROPERTY_KEY];
        Debug.Log(ReadyState[STATE_READY_PROPERTY_KEY]);
        photonView.RPC("CheckReady", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName, currentReady);
    }
    [PunRPC]
    public void CheckReady(string name, bool ready)
    {   
        
        UiMenuManager.instance.SetMarkReady(name,ready);
    }
    private void UpdateUiLocal()
    {
        UiMenuManager.instance.UiAddTeam(teamBlue, teamRed);
    }
    [PunRPC]
    public void UpdateUiAll()
    {
        UiMenuManager.instance.UiAddTeam(teamBlue, teamRed);
    }

    public void ClearALlBeforeOut()
    {
        teamBlue.Clear();
        teamRed.Clear();
    }

    public void LeftRoom()
    {
        photonView.RPC( "RemovePlayerTeam", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, Props[TEAM_PROPERTY_KEY].ToString());
        ClearALlBeforeOut();
        photonView.RPC("UpdateUiAll", RpcTarget.All);
        PhotonNetwork.LeaveRoom();
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            photonView.RPC("LeaveRoomAll", RpcTarget.All);
        }
    }
    public override void OnLeftRoom()
    {
        UiMenuManager.instance.openRoomManager.SetActive(false);
        UiMenuManager.instance.LobbyPanel.SetActive(false);
        UiMenuManager.instance.LoginPanel.SetActive(true);
       // Debug.Log("leave Room Success");
    }

    [PunRPC]
    public void AddPlayerTeam(string playerName, string nameTeam)
    {
        if(nameTeam == TEAM_BLUE_VALUE) teamBlue.Add(playerName);
        else teamRed.Add(playerName);
    }

    [PunRPC]
    public void RemovePlayerTeam(string playerName, string nameTeam)
    {
        if (nameTeam == TEAM_BLUE_VALUE) teamBlue.Remove(playerName);
        else teamRed.Remove(playerName);
    }
    [PunRPC]
    public void UpdateSwitchTeam(string playerName,string newTeam)
    {
        if(newTeam == TEAM_BLUE_VALUE)
        {
            teamBlue.Add(playerName);
            teamRed.Remove(playerName);
            Debug.Log("add blue");
        }
        else if(newTeam == TEAM_RED_VALUE) 
        {
            teamRed.Add(playerName);
            teamBlue.Remove(playerName);
            Debug.Log("add red");
        }
    }
    [PunRPC]
    public void LeaveRoomAll()
    {
        ClearALlBeforeOut();
        PhotonNetwork.LeaveRoom();
       

    }
   


}
