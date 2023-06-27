using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    private Camera camera;
    private PhotonView pv;
    private string colorTeam;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        camera = Camera.main;

    }
    private void Start()
    {
        if (!pv.IsMine) return;
        UpdatePropertiesPlayer.Instance.GetColor(ref colorTeam, pv.Owner.ActorNumber);
        SetPositionCamera();
    }
    void SetPositionCamera()
    {
        if (colorTeam == "blue")
            camera.transform.position = transform.position - new Vector3(0, -29.5f, /*14.59f*/0f);
        else
        {
            camera.transform.position = transform.position - new Vector3(0, -29.5f, /*-14.59f*/0f);
        }
    }
   
    void LateUpdate()
    {
        if (!pv.IsMine) return;
        if (camera!= null)
        {
            SetPositionCamera();
        }
    }
   
}
