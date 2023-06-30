using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListControlPannelRevival : MonoBehaviour
{
    public static ListControlPannelRevival instance;
    private void Awake() => instance = this;

    public void checkAndChooseEventDie(bool die, int photonView)
    {
        ControlPannelRevival[] _eventDie = FindObjectsOfType<ControlPannelRevival>();
        foreach (ControlPannelRevival eventDie in _eventDie)
        {
           // Debug.Log("photonView = " + photonView);
           // Debug.Log("ED_Pv = " + eventDie.photonView.Owner.NickName + eventDie.photonView.ViewID);
            if(eventDie.photonView.ViewID == photonView)
            {   
                eventDie.SetIsRevivalDie(die);
                Debug.Log("Check==");
            }
        }

    }
}
