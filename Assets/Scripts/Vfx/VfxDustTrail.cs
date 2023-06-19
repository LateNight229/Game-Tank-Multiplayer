using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class VfxDustTrail :  MonoBehaviourPun
{
    private ParticleSystem vfxDustTrailPartical;
    private PhotonView pv;
    private bool isMoving = false;
    

    private void Start()
    {
        vfxDustTrailPartical = GetComponent<ParticleSystem>();
        pv = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!pv.IsMine) return;
        float moveInput = Input.GetAxisRaw("Vertical");
        float turnInput = Input.GetAxisRaw("Horizontal");

        if (moveInput != 0 || turnInput != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        if (isMoving )
        {
            if (!vfxDustTrailPartical.isPlaying)
            {
                pv.RPC("PlayParticle", RpcTarget.All);
            }
        }
        else
        {
            if (vfxDustTrailPartical.isPlaying)
            {
                pv.RPC("StopParticle", RpcTarget.All);
            }
        }
    }
    [PunRPC]
    private void PlayParticle()
    {
        vfxDustTrailPartical.Play();
    }
    [PunRPC]
    private void StopParticle()
    {
        vfxDustTrailPartical.Stop();
    }
   
}
