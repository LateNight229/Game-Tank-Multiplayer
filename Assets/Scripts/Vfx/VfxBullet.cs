using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxBullet : MonoBehaviour
{

    private ParticleSystem vfxDustTrailPartical;
    private PhotonView pv;
    private void Start()
    {
        vfxDustTrailPartical = GetComponent<ParticleSystem>();
        pv = GetComponent<PhotonView>();
    }
    private void Update()
    {
        PlayParticle();
    }
    private void PlayParticle()
    {
        vfxDustTrailPartical.Play();
    }
}
