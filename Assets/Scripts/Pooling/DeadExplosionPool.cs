using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadExplosionPool : ExplosionPool
{
    public new static DeadExplosionPool Instance;

    private void Awake()
    {
        Instance = this;
        timeWaitExplosionFin = 2f;
    }
}
