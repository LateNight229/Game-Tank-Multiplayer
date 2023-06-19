using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiGameManager : Singleton<UiGameManager>
{
    public TextMeshProUGUI pingText;
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI ScoreBlue;
    public TextMeshProUGUI ScoreRed;

    private int scoreBlue = 0;
    private int scoreRed = 0;

    public void PingAndFPS(int ping, float fps)
    {
        pingText.text = "Ping: " +  ping.ToString() + " ms";
        fpsText.text = "FPS: " +  fps;
    }
    public void ScorebarUI(string color)
    {   
        //Debug.Log("Tank " + color + " UI die");
        if (color == "blue")
        {
            scoreRed++;
            ScoreRed.text = scoreRed.ToString();
            //Debug.Log("team Red + 1 Ponit!");
        }
        else if (color == "red")
        {
            scoreBlue++;
            ScoreBlue.text = scoreBlue.ToString();
            //Debug.Log("Team blue + 1 Point");
        }
        else return;
    }
}
