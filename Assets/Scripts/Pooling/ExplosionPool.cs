using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviourPunCallbacks
{
    public static ExplosionPool Instance;

    public GameObject explosionPrefab;
    public int poolSize = 10;
    protected float timeWaitExplosionFin = 2f;

    private List<GameObject> activeExplosions = new List<GameObject>();
    private List<GameObject> inactiveExplosions = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    protected virtual void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject explosion = Instantiate(explosionPrefab, Vector3.zero, Quaternion.identity);
            explosion.SetActive(false);
            inactiveExplosions.Add(explosion);
        }
    }

    public virtual void SpawnExplosion(Vector3 position)
    {
        GameObject explosion;

        if (inactiveExplosions.Count > 0)
        {
            explosion = inactiveExplosions[0];
            inactiveExplosions.RemoveAt(0);
        }
        else
        {
            explosion = Instantiate(explosionPrefab, Vector3.zero, Quaternion.identity);
        }

        explosion.transform.position = position;
        explosion.SetActive(true);
        activeExplosions.Add(explosion);

        StartCoroutine(WaitExplosionFinish(explosion, timeWaitExplosionFin));
    }

    private IEnumerator WaitExplosionFinish(GameObject explosion, float time)
    {
        yield return new WaitForSeconds(time);
        DisableExplosion(explosion);
    }

    public virtual void DisableExplosion(GameObject explosion)
    {
        if (activeExplosions.Contains(explosion))
        {
            if (!explosion.GetComponent<ParticleSystem>().isPlaying)
            {
                explosion.SetActive(false);
                activeExplosions.Remove(explosion);
                inactiveExplosions.Add(explosion);
            }
            else
            {
                // Xóa explosion chưa hoàn thành khỏi danh sách
                activeExplosions.Remove(explosion);

                // Tạo một explosion mới và thêm vào danh sách inactiveExplosions
                GameObject newExplosion = Instantiate(explosionPrefab, Vector3.zero, Quaternion.identity);
                newExplosion.SetActive(false);
                inactiveExplosions.Add(newExplosion);
            }
        }
    }
}
