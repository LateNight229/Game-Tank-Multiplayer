using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{   
    public static CameraFollow instance ;
    public Vector3 offset;
    private Camera camera;
    private PhotonView pv;
    private void Awake()
    {
        instance = this;
        pv = GetComponent<PhotonView>();
        camera = Camera.main;
    }
    private void Start()
    {  
        if (!pv.IsMine) return;
        SetPositionCamera();
    }
    void SetPositionCamera()
    {
        camera.transform.position = transform.position - new Vector3(0, -29.5f, 0f);
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
