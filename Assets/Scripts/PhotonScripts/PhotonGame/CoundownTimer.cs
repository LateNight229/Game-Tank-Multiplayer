using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoundownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    [SerializeField] private float totalTime = 20f; 

    private float currentTime;
    private bool isCountingDown = true;

    private void Start()
    {
        currentTime = totalTime;
        UpdateTimerText();
        StartCountdown();
    }

    private void Update()
    {
        if (isCountingDown && currentTime > 1)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();
        }
        if (currentTime == 0)
        {
            isCountingDown = false;
            HandleCountdownFinished();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    private void HandleCountdownFinished()
    {
        // Thực hiện hành động khi đếm ngược kết thúc
        
    }
    private void StartCountdown()
    {
    }
}
