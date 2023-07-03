using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IteamRecoverHealth : TimeItemAvailable
{
    private float healthRecoverAmount = 100f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health tankHealth = other.GetComponent<Health>();
            PhotonView healthPv = tankHealth.photonView;
            if(tankHealth != null )
            {
                if(PhotonNetwork.IsMasterClient)
                {
                    healthPv.RPC("MasterClientRecoverHealth", RpcTarget.MasterClient, healthRecoverAmount, healthPv.ViewID);
                }
                isTrigger = true;
            }
        }
    }
}
