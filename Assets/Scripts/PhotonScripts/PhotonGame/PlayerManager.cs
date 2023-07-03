using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{   
    public static PlayerManager instance;

    [SerializeField] private List<Transform> listPositonBlue = new List<Transform>();
    [SerializeField] private List<Transform> listPositonRed = new List<Transform>();
    private PhotonView pv;
    private Transform positionSpawn;
    private int postionIndex;
    private string colorTeam = "null";
    private string colorTank;
    private string colorTurret;

    private void Awake()
    {
        instance = this;
        pv = GetComponent<PhotonView>();
    }
    void Start()
    {
        if (pv.IsMine)
        {
            UpdatePropertiesPlayer.Instance.GetColorAndPositionTankLocal(ref colorTeam, ref postionIndex);
            UpdateColorTank();
            UpdateColorTurret();
            UpdatepositionSpawnTank();
            CreateTankController();
            CreateTurretController();
        }
    }
    void CreateTankController()
    {
        GameManager.instance.CreateObj("PhotonPrefabs", "TankStorage",colorTank,positionSpawn.position, positionSpawn.rotation); 
    }
    void CreateTurretController()
    {
        GameManager.instance.CreateObj("PhotonPrefabs",
         "TurretStorage", colorTurret, positionSpawn.position, positionSpawn.rotation);
    }
    string UpdateColorTurret()
    {
        colorTurret = colorTeam == "blue" ? "TurretBlueController" : "TurretRedController";
        return colorTurret;
    }
    string UpdateColorTank()
    {
        colorTank = colorTeam == "blue" ? "TankBlueController" : "TankRedController";
        return colorTank;
    }
      Transform UpdatepositionSpawnTank()
    {
        if (colorTank == "TankBlueController")
        {   
            positionSpawn = listPositonBlue[postionIndex - 1];
        }
        else
        {
            positionSpawn = listPositonRed[postionIndex - 1];
        }
        return positionSpawn;
    }
    public Transform RepositionPlayer(int positionIndex,ref Transform positionSpawn, string color )
    {   if (color == "blue")
            positionSpawn = listPositonBlue[positionIndex - 1];
        else
        {
            positionSpawn = listPositonRed[positionIndex - 1];
        }
        return positionSpawn;
    }

}
