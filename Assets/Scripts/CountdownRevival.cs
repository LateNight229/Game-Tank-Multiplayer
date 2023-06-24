using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownRevival : CoundownTimer
{   
    public static CountdownRevival Instance;

    public GameObject panel;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }
    protected override void Start()
    {
    }
    protected override void HandleCountdownFinished()
    {
        panel.SetActive(false);
    }

    protected override void UpdateTimerText() => timerText.text = currentTime.ToString("0");
    public void StartCountDown()
    {
        panel.SetActive(true);
        InitializeCountdown();
        isCountingDown = true;
        UpdateTimerText();
    }
    private void InitializeCountdown()
    {
        totalTime = 3f;
        timeFinish = 1;
        currentTime = totalTime;
    }
}
