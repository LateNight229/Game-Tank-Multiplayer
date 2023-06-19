using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float turnSpeed = 240.0f;
    [SerializeField] private float wheelSpeed = 500.0f;

    [Header(" 4 wheel")]
    public Transform leftWheel;
    public Transform rightWheel;
    public Transform leftWheelB;
    public Transform rightWheelB;


    float moveInput;
    private PhotonView PV;
    private bool isLocalPlayer = false;

    public bool IsLocalPlayer
    {
        get { return isLocalPlayer; }
        set { isLocalPlayer = value; }
    }
    void Awake()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            IsLocalPlayer = true;
            Debug.Log("IslocalPlayer = true");
        }

    }
    private void Start()
    {
       
    }
    void FixedUpdate()
    {
        if (!PV.IsMine) return;

        Movement();
    }
    void Movement()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");
        Vector3 moveDirection = transform.forward * moveInput;
        transform.Translate(moveDirection * moveSpeed * Time.fixedDeltaTime, Space.World);
        if(moveInput != 0)
        {
            Quaternion turnAngle = Quaternion.Euler(0.0f, turnInput * turnSpeed * Time.fixedDeltaTime, 0.0f);
            transform.rotation *= turnAngle;
            RotateWheel(moveInput );

        }
    }
    void RotateWheel(float moveInput)
    {   
        float wheelRotation = moveInput * wheelSpeed * Time.fixedDeltaTime;
        leftWheel.Rotate(wheelRotation, 0, 0);
        rightWheel.Rotate(wheelRotation, 0, 0);
        leftWheelB.Rotate(wheelRotation, 0, 0);
        rightWheelB.Rotate(wheelRotation, 0, 0);
    }
}
