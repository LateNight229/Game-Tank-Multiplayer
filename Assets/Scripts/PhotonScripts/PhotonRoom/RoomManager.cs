using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject openRoomManager;
    [SerializeField] private TextMeshProUGUI RoomName;
    [SerializeField] private TextMeshProUGUI RoomNameSuccess;
    [SerializeField] private TMP_InputField inputRoomName;

    private const int MAX_ROOM_PLAYER = 6;

    private string roomName;


    private void Start()
    {
        string savedRoomName = PlayerPrefs.GetString("SavedRoomName");
        if (!string.IsNullOrEmpty(savedRoomName))
        {
            inputRoomName.text = savedRoomName;
        }
        Debug.Log("autoJoinCount = " + PlayerPrefs.GetInt("AutoJoinRoom", 0));
       if(PlayerPrefs.GetInt("AutoJoinRoom", 0) == 1)
        {
            PlayerPrefs.SetInt("AutoJoinRoom", 0);
            CreatRoom();
        }
        CreatRoom();
    }
    public void SaveRoomName()
    {
        string roomName = inputRoomName.text;
        PlayerPrefs.SetString("SavedRoomName", roomName);
        PlayerPrefs.Save();
    }

    public void CreatRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            roomName = inputRoomName.text;
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = (byte)MAX_ROOM_PLAYER });
            Debug.Log("Creat Room " + roomName);

        }
        else
        {
            UiMenuManager.instance.ShowMessage("Not Connected to server yet !");
        }

    }
    public void JoinRoom()
    {
        roomName = inputRoomName.text;
        PhotonNetwork.JoinRoom(roomName);
    }
    public override void OnCreatedRoom()
    {
    }
    public override void OnJoinedRoom()
    {   
        UiMenuManager.instance.LobbyPanel.SetActive(false);
        openRoomManager.SetActive(true);
        RoomNameSuccess.text = roomName.ToString();
        TeamManager.instance.UpdatePlayerAvailable();
        TeamManager.instance.NotifiJoinRoom();

    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        string _message = "Room creation failed: " + message;
        UiMenuManager.instance.ShowMessage(_message);
        JoinRoom();
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        string _message = "Room join failed : " + message;
        UiMenuManager.instance.ShowMessage(_message);
    }


}
