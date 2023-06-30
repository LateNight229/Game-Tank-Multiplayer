using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiGameManager : MonoBehaviourPunCallbacks
{
    public static UiGameManager Instance;    
    //public TextMeshProUGUI pingText;
    //public TextMeshProUGUI fpsText;
    public TextMeshProUGUI ScoreBlueText;
    public TextMeshProUGUI ScoreRedText;

    private int scoreBlue = 0;
    private int scoreRed = 0;

    private void Awake()
    {
        Instance = this;
    }
    //public void PingAndFPS(int ping, float fps)
    //{
    //    pingText.text = "Ping: " +  ping.ToString() + " ms";
    //    fpsText.text = "FPS: " +  fps;
    //}
    public void ScorebarUI(string color)
    {   
        //Debug.Log("Tank " + color + " UI die");
        if (color == "blue")
        {
            scoreRed++;
            ScoreRedText.text = scoreRed.ToString();
            //Debug.Log("team Red + 1 Ponit!");
        }
        else if (color == "red")
        {
            scoreBlue++;
            ScoreBlueText.text = scoreBlue.ToString();
            //Debug.Log("Team blue + 1 Point");
        }
        else return;
    }
    public void GetScore(ref int blue,ref int red)
    {
        blue = scoreBlue; red = scoreRed;
    }
}
