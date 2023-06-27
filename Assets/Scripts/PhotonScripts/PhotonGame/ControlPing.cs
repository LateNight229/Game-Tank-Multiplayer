using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class ControlPing : MonoBehaviourPunCallbacks
{
    private float fpsMeasurePeriod = 0.5f;
    private float fpsNextPeriod = 0f;
    private int framesInCurrentPeriod = 0;
    float fps;
    private void Start()
    {
        fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
    }
    private void Update()
    {
        framesInCurrentPeriod++;

        if (Time.realtimeSinceStartup > fpsNextPeriod)
        {
            fps = Mathf.RoundToInt(framesInCurrentPeriod / fpsMeasurePeriod);
            framesInCurrentPeriod = 0;
            fpsNextPeriod += fpsMeasurePeriod;
        }

        int  ping = PhotonNetwork.GetPing();
        //UiGameManager.Instance.PingAndFPS(ping, fps);

    }

}
