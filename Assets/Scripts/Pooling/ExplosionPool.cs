using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviourPunCallbacks
{
    public static ExplosionPool Instance;

    public GameObject explosionPrefab;
    public int poolSize = 10;
    protected  float timeDisableExplosion = 0.5f;

    private GameObject[] explosionPool;
    private int currentExplosion = 0;

    private void Awake()
    {
        Instance = this;
    }
    protected virtual void Start()
    {   
        explosionPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            explosionPool[i] = Instantiate(explosionPrefab, Vector3.zero, Quaternion.identity);
            explosionPool[i].SetActive(false);
        }
    }

    public virtual void  SpawnExplosion(Vector3 position)
    {
        
        explosionPool[currentExplosion].transform.position = position;
        explosionPool[currentExplosion].SetActive(true); 
        currentExplosion = (currentExplosion + 1) % poolSize;
        StartCoroutine(SpawnExplosion(timeDisableExplosion));

    }

    private IEnumerator SpawnExplosion(float time)
    {
        yield return new WaitForSeconds(time);
        DisableExplosion();
    }
    public virtual void DisableExplosion()
    {   
        if(currentExplosion == 0)
        {
            explosionPool[currentExplosion + poolSize - 1].SetActive(false);
        }else
        explosionPool[currentExplosion -1 ].SetActive(false);
       
    }
}
