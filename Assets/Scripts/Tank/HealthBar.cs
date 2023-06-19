using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviourPunCallbacks
{
    public static HealthBar instance;

    [SerializeField] private Image filledBar;

    private PhotonView pv;
    private void Awake()
    {
        instance = this;
        pv = GetComponent<PhotonView>();
    }
    public void UpdateHealthBar(float filledAmount, int viewHealthID)
    {
        //Debug.Log("Pv.ViewID = " + pv.ViewID);
        //Debug.Log("Viewbar = " + viewHealthID);
        if (viewHealthID == pv.ViewID)
        {
            filledBar.fillAmount = filledAmount;
           // photonView.RPC("UpdateHealthBarRPC", RpcTarget.All, filledAmount);
            //Debug.Log("id = id");
        }
        else return ;
    }
    [PunRPC]
    public void UpdateHealthBarRPC(float filledAmount)
    {
        filledBar.fillAmount = filledAmount;
    }
}
