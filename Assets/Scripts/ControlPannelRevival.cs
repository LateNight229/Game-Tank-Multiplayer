using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPannelRevival : MonoBehaviourPunCallbacks
{   
    public static ControlPannelRevival instance;    

    private PhotonView pv;
    private PhotonView viewID;
    private bool isRevivalDie;
    private void Awake()
    {   
        instance = this;
        pv = GetComponent<PhotonView>();
    }
    void Start()
    {
        
    }
    public void SetIsRevivalDie(bool value )
    {
        isRevivalDie = value;
       // Debug.Log("SetIsRevivalDie = " + isRevivalDie.ToString() +"pv = "+pv.Owner.NickName);
    }
    // Update is called once per frame
    void Update()
    {
        if (!pv.IsMine) return;
        if(isRevivalDie == true )
        {
            CountdownRevival.Instance.StartCountDown();
            Debug.Log("Start Coundown !");
            isRevivalDie=false;
        }
    }
}
