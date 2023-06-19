using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class ObjPooling : MonoBehaviourPunCallbacks
{
    public static ObjPooling instance;
    public List<GameObject> bulletsBlue = new List<GameObject>();
    public List<GameObject> bulletsRed = new List<GameObject>();

    [SerializeField] private int poolSize;
    [SerializeField] private GameObject prefabBulletBlue;
    [SerializeField] private GameObject prefabBulletRed;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {   
        CreateBullet();
    }
    void CreateBullet()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bulletB = Instantiate(prefabBulletBlue, transform.position, Quaternion.identity);
            bulletB.SetActive(false);
            bulletsBlue.Add(bulletB);
            GameObject bulletR = Instantiate(prefabBulletRed, transform.position, Quaternion.identity);
            bulletR.SetActive(false);
            bulletsRed.Add(bulletR);
        }
    }
    public GameObject GetBullet(Vector3 position, Quaternion rotation, bool blue)
    {
        List<GameObject> bullets = blue ? bulletsBlue : bulletsRed;
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                bullet.transform.position = position;
                bullet.transform.rotation = rotation;
                return bullet;
            }
        }
        return null;
    }
    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
    }
}
