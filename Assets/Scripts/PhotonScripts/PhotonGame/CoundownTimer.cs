using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoundownTimer : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI timerText;
    protected float totalTime = 3f;

    protected float currentTime;
    protected bool isCountingDown = true;
    protected int timeFinish = 1;
    private bool isLoadedScene;

    protected virtual void Start()
    {
        currentTime = totalTime;
        StartCountdown();
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
        Debug.Log("Current Time, isCTD : " + currentTime.ToString() + " - " + isCountingDown.ToString());
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
        LoadSceneEndPlay();
    }
    //[PunRPC]
    public void LoadSceneEndPlay()
    {
        PhotonNetwork.LoadLevel(2);
    }
    protected virtual void StartCountdown()
    {

    }
}
