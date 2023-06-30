using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStart : Singleton<playerStart>
{
    private Camera Camera;

    private void Awake()
    {
        Camera = Camera.main;
    }

    public Camera GetCamera()
    {
        return Camera;
    }
}
