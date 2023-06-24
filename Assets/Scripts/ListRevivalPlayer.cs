using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListRevivalPlayer : MonoBehaviour
{   
    public static ListRevivalPlayer Instance;

    private void Awake()
    {
        Instance = this;
    }
    //private RevivalPlayer revival;

    public List<RevivalPlayer> activeRevivalPlayer = new List<RevivalPlayer>();

    public void AddActive(RevivalPlayer revival)
    {
        activeRevivalPlayer.Add(revival);
    }
    public void GetRevival(int OwnerActorNumber, string color)
    {
        if(activeRevivalPlayer.Count > 0)
        {
            RevivalPlayer revival = activeRevivalPlayer[0];
            activeRevivalPlayer.Remove(revival);
            revival.ResetPlayerPosition(OwnerActorNumber, color);
        }
    }
}
