using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{   
    public static UiManager instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
    }
    [Header("List Text Team") ]
    [SerializeField] private List<TextMeshProUGUI> listTeamBlue;
    [SerializeField] private List<TextMeshProUGUI> listTeamRed;
    [SerializeField] private TextMeshProUGUI debugSwitchTeam;

    [Header("List Toogle Ready")]
    [SerializeField] private List<GameObject> listToogleReadyBlue;
    [SerializeField] private List<GameObject> listToogleReadyRed;
    
    [Header("State Ready")]
    [SerializeField] private List<GameObject> markReadyBlue;
    [SerializeField] private List<GameObject> markReadyRed;
    [SerializeField] private TextMeshProUGUI textReady;

    [Header("Pannel")]
    public GameObject openRoomManager;
    public GameObject LobbyPanel;
    public GameObject LoginPanel;

    private const string EMPTY_STRING = "";
    public void UiAddTeam(List<string> teamBlue, List<string> teamRed)
    {
        UiAddTeamBlue(teamBlue);
        UiAddTeamRed(teamRed);
        UiToogleReadyBlue(teamBlue);
        UiToogleReadyRed(teamRed);
        
    }
    public void UiAddTeamBlue(List<string> teamBlue)
    {
        int count = Mathf.Min(teamBlue.Count, listTeamBlue.Count);
        for(int i = 0; i < count; i++) 
        {
            listTeamBlue[i].text = teamBlue[i];
        }
        for(int i = count; i < listTeamRed.Count; i++)
        {
            listTeamBlue[i].text = EMPTY_STRING;
        }
        
    }
    public void UiAddTeamRed(List<string> teamRed)
    {
        int count = Mathf.Min(teamRed.Count, listTeamRed.Count);
        for (int i = 0; i < count; i++)
        {
            listTeamRed[i].text = teamRed[i];
        }
        for (int i = count; i < listTeamRed.Count; i++)
        {
            listTeamRed[i].text = EMPTY_STRING;
        }

    }

    public void UiToogleReadyBlue(List<string> teamBlue)
    {   
        int count = Mathf.Min(teamBlue.Count,listTeamBlue.Count);
        for (int i = 0; i < count; i++)
        {
            listToogleReadyBlue[i].gameObject.SetActive(true);
        }
        for (int i = count; i < listTeamBlue.Count; i++)
        {
            listToogleReadyBlue[i].gameObject.SetActive(false);
        }
    }
    public void UiToogleReadyRed(List<string> teamRed)
    {
        int count = Mathf.Min(teamRed.Count, listTeamRed.Count);
        for (int i = 0; i < count; i++)
        {
            listToogleReadyRed[i].gameObject.SetActive(true);

        }
        for (int i = count; i < listTeamRed.Count; i++)
        {
            listToogleReadyRed[i].gameObject.SetActive(false);
        }
    }
    public void SetMarkReady(string playerName, bool isReady)
    {   
        if(playerName == PhotonNetwork.LocalPlayer.NickName)
        {
            if (isReady == true) { textReady.text = "UnReady";  } else textReady.text = "Ready";
        }
        foreach (TextMeshProUGUI player in listTeamBlue)
        {
            if (player.text == playerName)
            {
                markReadyBlue[listTeamBlue.IndexOf(player)].SetActive(isReady);
                break;
            }
        }
        foreach (TextMeshProUGUI player in listTeamRed)
        {
            if (player.text == playerName)
            {
                markReadyRed[listTeamRed.IndexOf(player)].SetActive(isReady);
                break;
            }
        }
    }
    public void UpdateMarkWhenSwitchTeam(string playerName, bool isReady )
    {
        foreach (TextMeshProUGUI player in listTeamBlue)
        {
            if (player.text == playerName)
            {
                markReadyBlue[listTeamBlue.IndexOf(player)].SetActive(isReady);
                break;
            }
        }
        foreach (TextMeshProUGUI player in listTeamRed)
        {
            if (player.text == playerName)
            {
                markReadyRed[listTeamRed.IndexOf(player)].SetActive(isReady);
                break;
            }
        }
    }
}
