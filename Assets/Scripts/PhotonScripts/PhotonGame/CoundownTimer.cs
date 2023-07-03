using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class CoundownTimer : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI timerText;
    public List<Transform> posItem ;
    public List<GameObject> items ;
    

    protected float totalTime = 180f;
    protected float currentTime;
    protected int timeFinish = 1;

    private bool isLoadedScene;
    protected bool isCountingDown = true;
    private bool dropItem1;
    private bool dropItem2;
    private bool dropItem3;
    private PhotonView pv;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    protected virtual void Start()
    {   
        currentTime = totalTime;
        UpdateTimerText();
    }

    protected virtual void Update()
    {
        if (isCountingDown && currentTime > 1)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();
        }
        if (currentTime <= timeFinish && isLoadedScene == false)
        {
            isCountingDown = false;
            HandleCountdownFinished();
        }
        if (dropItem1 == false && isCountingDown && currentTime <= 150f)
        {
            dropItem1 = true;
            Transform posDropItem = GetRandomTranform();
            string itemName = GetRandomItemName();
            SpawnItem(posDropItem, itemName);
        }
        if (dropItem2 == false && isCountingDown && currentTime <= 120f)
        {
            dropItem2 = true;
            Transform posDropItem = GetRandomTranform();
            string itemName = GetRandomItemName();
            SpawnItem(posDropItem, itemName);
        }
        if (dropItem3 == false && isCountingDown && currentTime <= 90f)
        {
            dropItem3 = true;
            Transform posDropItem = GetRandomTranform();
            string itemName = GetRandomItemName();
            SpawnItem(posDropItem, itemName);
        }
    }
    private void SpawnItem(Transform posItem, string item)
    {
        PhotonNetwork.Instantiate(Path.Combine("List_Item_Runtime", item), posItem.position, posItem.rotation);
        //PhotonNetwork.Instantiate(Path.Combine("List_Item_Runtime", "Speed"), posItem.position, posItem.rotation);

    }
    private string GetRandomItemName()
    {
        int randomItem = Random.Range(0, items.Count);
        return items[randomItem].name;
    }
    private Transform GetRandomTranform()
    {
        int randomPosDropItem =Random.Range(0, posItem.Count );
        return posItem[randomPosDropItem];
    }
    protected virtual void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    protected virtual void HandleCountdownFinished()
    {
        isLoadedScene = true;
        pv.RPC("LoadSceneEndPlayMC", RpcTarget.MasterClient);
    }
    [PunRPC]
    public void LoadSceneEndPlayMC()
    {
        pv.RPC("LoadSceneEndPlay", RpcTarget.All);
    }
    [PunRPC]
    public void LoadSceneEndPlay()
    {
        PhotonNetwork.LoadLevel(2);
    }
}
