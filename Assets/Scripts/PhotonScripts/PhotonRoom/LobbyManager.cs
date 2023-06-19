using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;

public class LobbyManager : MonoBehaviourPunCallbacks
{   
    public static LobbyManager instance;
    public const string NAME_PLAYER = "name";

    [SerializeField] private GameObject joinLobbyButton;
    [SerializeField] private TextMeshProUGUI PlayerName;
    [SerializeField] private TMP_InputField inputPlayerName;
    [SerializeField] private int nameLength = 8;
    [SerializeField] private string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    ExitGames.Client.Photon.Hashtable NamePlayer = new ExitGames.Client.Photon.Hashtable();

    private string playerName;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {   
        PlayerName.text = "Player Name: ";
        string randonName = GenerateRandomName(nameLength,allowedChars);
        inputPlayerName.text = randonName;
        NamePlayer[NAME_PLAYER] = randonName;
        PhotonNetwork.SetPlayerCustomProperties(NamePlayer);
        ConnectToMaster();
    }
    string GenerateRandomName(int length, string allowedChars)
    {
        char[] chars = new char[length];
        System.Random random =  new System.Random();
        for(int i = 0; i < length; i++)
        {
            chars[i] = allowedChars[random.Next(0,allowedChars.Length)];
        }
        return new string(chars);
    }
    public void ConnectToMaster()
    {
        playerName = inputPlayerName.text;
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.LocalPlayer.NickName = playerName;
        }
    }
    public void LeftLobby()
    {
        PhotonNetwork.LeaveLobby();
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public override void OnLeftLobby()
    {
        UiManager.instance.LobbyPanel.SetActive(false);
        UiManager.instance.LoginPanel.SetActive(true);
        Debug.Log("Leaved LobbySuccess");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server!");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby!");
        UiManager.instance.LoginPanel.SetActive(false);
        UiManager.instance.LobbyPanel.SetActive(true);
    }

}
