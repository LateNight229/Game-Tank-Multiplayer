using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TurretController : MonoBehaviourPunCallbacks
{
    [SerializeField] private float distanceFromCamera;
    [SerializeField] private float turnSpeed = 20f;
    private Vector3 turretTranformPosition = new Vector3(0, 0.4f, 0);

    private Camera mainCamera;
    private Transform turretTransform;
    private PhotonView pv;
    private PhotonView tankPV;

    private bool pause;
    private void Awake()
    {
        turretTransform = transform;
        pv = GetComponent<PhotonView>();
    }
    void Start()
    {
        CheckOwnerMyTank();
    }
    private PhotonView photonViewTank;
    void CheckOwnerMyTank()
    {
        if (pv.Owner == PhotonNetwork.LocalPlayer )
        {
            foreach ( PhotonView view in PhotonNetwork.PhotonViews)
            {
                if(view.tag == "Player" && view.Owner == pv.Owner)
                {
                    tankPV = view; break;
                }
            }
        }
    }

    private void Update()
    {  
        if (!pv.IsMine) { return; }
        if(Input.GetKeyDown(KeyCode.P)) 
        {
            pause = !pause;
        }
        RotationFollowMouse();
        UpdatePositionFollowTank();
    }
    void RotationFollowMouse()
    {   
        if(pause == true) { return; }
        mainCamera = playerStart.Instance.Camera;
        Vector3 mousePos = Input.mousePosition;
        Vector3 planeNormal = Vector3.up;
        Plane plane = new Plane(planeNormal, distanceFromCamera);
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        float rayDistance;
        if (plane.Raycast(ray, out rayDistance))
        {
            Vector3 worldPos = ray.GetPoint(rayDistance);
            Vector3 gunDirection = worldPos - turretTransform.position;
            gunDirection.y = 0;
            Quaternion gunRotation = Quaternion.LookRotation(gunDirection);
            float gunAngle = Quaternion.Angle(turretTransform.rotation, gunRotation);
            Quaternion targetGunRotation = Quaternion.Slerp(turretTransform.rotation, gunRotation, Time.deltaTime * turnSpeed);
            if (gunAngle > 0.3f)
            {
                turretTransform.rotation = targetGunRotation;
            }
        }
    }
    void UpdatePositionFollowTank()
    {
        transform.position = tankPV.gameObject.transform.position + turretTranformPosition;
    }
}
