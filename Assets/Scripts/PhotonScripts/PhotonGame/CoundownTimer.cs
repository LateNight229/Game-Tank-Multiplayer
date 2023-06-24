using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoundownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    protected float totalTime = 20f;

    protected float currentTime;
    protected bool isCountingDown = true;
    protected int timeFinish = 0;

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
        if (currentTime <= timeFinish)
        {
            isCountingDown = false;
            HandleCountdownFinished();
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
    }
    protected virtual void StartCountdown()
    {

    }
}
