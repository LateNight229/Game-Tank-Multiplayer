using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoundownTimer : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI timerText;
    public GameObject itemHealth;
    

    protected float totalTime = 30f;
    protected float currentTime;
    protected bool isCountingDown = true;
    protected int timeFinish = 1;

    private bool isLoadedScene;
    private PhotonView pv;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    protected virtual void Start()
    {   
        currentTime = totalTime;
        UpdateTimerText();
    }

    protected virtual void Update()
    {
        if (isCountingDown && currentTime > 1)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();
        }
        if (currentTime <= timeFinish && isLoadedScene == false)
        {
            isCountingDown = false;
            HandleCountdownFinished();
        }
        if(isCountingDown && currentTime <= totalTime / 3)
        {
            
        }
    }

    protected virtual void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    protected virtual void HandleCountdownFinished()
    {
        isLoadedScene = true;
        pv.RPC("LoadSceneEndPlayMC", RpcTarget.MasterClient);
    }
    [PunRPC]
    public void LoadSceneEndPlayMC()
    {
        pv.RPC("LoadSceneEndPlay", RpcTarget.All);
    }
    [PunRPC]
    public void LoadSceneEndPlay()
    {
        PhotonNetwork.LoadLevel(2);
    }
}
