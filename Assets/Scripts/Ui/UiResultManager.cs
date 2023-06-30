using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiResultManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI ScoreBlueText;
    public TextMeshProUGUI ScoreRedText;
    public TextMeshProUGUI ResultMatchText;


    [Header("List Text Team")]
    [SerializeField] private List<TextMeshProUGUI> listTeamBlue;
    [SerializeField] private List<TextMeshProUGUI> listTeamRed;
    [SerializeField] private TextMeshProUGUI SendMessageText;

    private int scoreBlue = 0;
    private int scoreRed = 0;
    private int timeDeleteMessage = 3;
    private void Start()
    {
        UiGameManager.Instance.GetScore(ref scoreBlue, ref scoreRed);
        ScoreBlueText.text = scoreBlue.ToString();
        ScoreRedText.text = scoreRed.ToString();
        ResultMatch(scoreBlue, scoreRed);
        GetListPlayerEachTeam();
    }
    public void ShowMessage(string message)
    {
        SendMessageText.text = message;
        Invoke("DeletMessage", timeDeleteMessage);
    }
    void DeletMessage()
    {
        SendMessageText.text = "";
    }
    void ResultMatch(int scoreBlue, int scoreRed)
    {
        if(scoreBlue > scoreRed) { ResultMatchText.text = "Blue Win"; }
        else if(scoreBlue < scoreRed) { ResultMatchText.text = "Red Win"; }
        else { ResultMatchText.text = "Duel"; }

    }
    void GetListPlayerEachTeam()
    {
        UiMenuManager.instance.GetListTeamBlue(ref listTeamBlue, ref listTeamRed);
    }
    public void ClickExit()
    {   
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
    }
    public void ClickPlayerAgain()
    {
        if (!IsRoomMaster())
        {
            ShowMessage("You are not the room owner !");
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
        SetAutoJoinRoom(1);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
    }
    public void SetAutoJoinRoom(int count)
    {
        PlayerPrefs.SetInt("AutoJoinRoom", count);
    }
    public override void OnLeftRoom()
    {
        Debug.Log("LeaveRoomSuccess");
        PhotonNetwork.LoadLevel(0);

    }
}
