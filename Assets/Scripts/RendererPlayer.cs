using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RendererPlayer : MonoBehaviourPunCallbacks
{
    public static RendererPlayer Instance;

    void Awake()
    {
        Instance = this;
    }

    public void SetRendererPlayer()
    {

    }
}
