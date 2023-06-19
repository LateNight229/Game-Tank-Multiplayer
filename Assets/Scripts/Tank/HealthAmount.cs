using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthAmount : MonoBehaviourPunCallbacks
{   
    public static HealthAmount instance;
   
    public TextMeshProUGUI healthValue;

    private PhotonView pv;
    private void Awake()
    {
        instance = this;
        pv = GetComponent<PhotonView>();
    }
    public void UpdateHealthAmount(float healthAmount,int viewHealthID)
    {
        if (viewHealthID == pv.ViewID)
        {
            healthValue.text = healthAmount.ToString();
            //photonView.RPC("UpdateHealthAmountRPC", RpcTarget.All, healthAmount);
        }
        else return;
    }
    [PunRPC]
    public void UpdateHealthAmountRPC(float currentHealth )
    {
        healthValue.text = currentHealth.ToString();
    }
}
