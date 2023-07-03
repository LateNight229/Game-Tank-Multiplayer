using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownRevival : MonoBehaviour
{
    public static CountdownRevival Instance;

    public TextMeshProUGUI timerText;
    public GameObject panel;

    protected bool isCountingDown = true;
    protected float totalTime = 3f;
    protected int timeFinish = 1;
    protected float currentTime;


    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }
    private void Start()
    {
    }
    private void HandleCountdownFinished()
    {
        panel.SetActive(false);
    }

    private void UpdateTimerText() => timerText.text = currentTime.ToString("0");
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
    private void Update()
    {
        if (isCountingDown && currentTime > 1)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();
        }
        if (currentTime <= timeFinish )
        {
            isCountingDown = false;
            HandleCountdownFinished();
        }
    }
}
