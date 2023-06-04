using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenPast : MonoBehaviour
{
    public float positionToDestroyAt;
    public bool less;
    public Transform tr;
    public string objectIndex;
    public GameObject destroyingObj;
    private void FixedUpdate()
    {
        if (less)
        {
            if (tr.position.x <= positionToDestroyAt)
            {
                PoolingManager.instance.Destr(destroyingObj.name, objectIndex);
            }
        }
        else
        {
            if (tr.position.x >= positionToDestroyAt)
            {
                PoolingManager.instance.Destr(destroyingObj.name, objectIndex);
            }
        }
    }
}
