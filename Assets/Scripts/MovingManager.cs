using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingManager : MonoBehaviour
{
    [SerializeField] private movingGroup[] movingObjects;
    
    private void Start()
    {
        Application.targetFrameRate = 120;
        for (int i = 0; i < movingObjects.Length; i++)
        {
            for (int j = 0; j < movingObjects[i].prerunAmount; j++)
            {
                GameObject spawnedObj = PoolingManager.instance.Inst(movingObjects[i].movingObjectIndex,
                    movingObjects[i].referenceTransform.position,
                    movingObjects[i].referenceTransform.rotation);
                spawnedObj.GetComponent<MovingObject>().initialize(movingObjects[i].movingSpeed *
                                                                   GameDifficulty.instance.getValue(
                                                                       movingObjects[i].movingObjectIndex + "Speed",
                                                                       1), movingObjects[i].distanceBetweenObjects * (j + 1));
            }
            StartCoroutine(cycleObjects(movingObjects[i], 0));
        }
    }

    private IEnumerator cycleObjects(movingGroup moveGroup, int curCycle)
    {
        GameObject spawnedObj = PoolingManager.instance.Inst(moveGroup.movingObjectIndex,
            moveGroup.referenceTransform.position,
            moveGroup.referenceTransform.rotation);
        spawnedObj.GetComponent<MovingObject>().initialize(moveGroup.movingSpeed * GameDifficulty.instance.getValue(moveGroup.movingObjectIndex + "Speed", 1));
        Component dataReceiver;
        if (spawnedObj.TryGetComponent(typeof(IReceiveCycleData), out dataReceiver))
        {
            (dataReceiver as IReceiveCycleData).passData(curCycle);
        }
        int time = Mathf.FloorToInt((moveGroup.distanceBetweenObjects*GameDifficulty.instance.getValue(moveGroup.movingObjectIndex + "Distance", 1))  / (moveGroup.movingSpeed* GameDifficulty.instance.getValue(moveGroup.movingObjectIndex + "Speed", 1)));
        for (int t = 0; t < time;)
        {
            yield return new WaitForFixedUpdate();
            if(GameStateManager.instance.getState() == GameState.Play)
                t++;
        }

        StartCoroutine(cycleObjects(moveGroup, curCycle + 1));
    }
}

[System.Serializable]
public struct movingGroup
{
    public string movingObjectIndex;
    public Transform referenceTransform;
    public float distanceBetweenObjects;
    public float movingSpeed;
    public int prerunAmount;
}