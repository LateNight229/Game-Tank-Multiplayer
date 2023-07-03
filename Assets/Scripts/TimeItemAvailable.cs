using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeItemAvailable : MonoBehaviour
{

    protected  float timeStart;
    protected float timeAvailable = 15f;
    protected bool isTrigger;
    protected virtual void Start()
    {
        timeStart = timeAvailable;
    }
    protected virtual void Update()
    {
        timeStart -= Time.deltaTime;
        if (timeStart <= 0 || isTrigger == true)
        {
            Destroy(gameObject);
        }
    }

}
