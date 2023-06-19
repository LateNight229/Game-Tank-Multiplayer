using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
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
        RoomName.text = "Room Name: ";
        inputRoomName.text = "Room1";

        CreatRoom();

    }

  
    public void CreatRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {   
            roomName = inputRoomName.text;
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = (byte)MAX_ROOM_PLAYER });
            Debug.Log("Creat Room " +  roomName);

        }
        else
        {
            Debug.Log("Not Connected to server yet !");
       }

    }
    public void JoinRoom()
    {
        roomName = inputRoomName.text;
        PhotonNetwork.JoinRoom(roomName);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Room created successfully!");
        

    }
    public override void OnJoinedRoom()
    {   
        UiManager.instance.LobbyPanel.SetActive(false);
        openRoomManager.SetActive(true);
        RoomNameSuccess.text = roomName.ToString();
        TeamManager.instance.UpdatePlayerAvailable();
        TeamManager.instance.NotifiJoinRoom();

    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed: " + message); 
        JoinRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
    }
   
}
