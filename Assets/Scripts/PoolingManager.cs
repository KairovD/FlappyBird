using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public initialPrefab[] initialPrefabs;
    public static PoolingManager instance;
    [SerializeField] private PoolingPrefab[] poolingPrefabs;
    private void Awake()
    {
        instance = this;
        //StartCoroutine(syncDestroyedObjects());
        poolingPrefabs = new PoolingPrefab[initialPrefabs.Length];
        for (int i = 0; i < initialPrefabs.Length; i++)
        {
            GameObject[] InstantiatedPrefabs = new GameObject[initialPrefabs[i].count];
            for (int j = 0; j < initialPrefabs[i].count; j++)
            {
                InstantiatedPrefabs[j] = Instantiate(initialPrefabs[i].Prefab);
                InstantiatedPrefabs[j].name = initialPrefabs[i].Prefab.name + j;
                InstantiatedPrefabs[j].SetActive(false);
            }
            
            poolingPrefabs[i] = new PoolingPrefab(initialPrefabs[i].Prefab, initialPrefabs[i].PrefabIndex, InstantiatedPrefabs);
        }
    }
    

    public GameObject Inst(string PrefabIndex, Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < poolingPrefabs.Length; i++)
        {
            if (poolingPrefabs[i].PrefabIndex != PrefabIndex) continue;
            
            if (poolingPrefabs[i].FreePrefabs.Count <= 0)
            {
                GameObject[] InstantiatedPrefabs = poolingPrefabs[i].Prefabs;
                poolingPrefabs[i].Prefabs = new GameObject[poolingPrefabs[i].Prefabs.Length + 20];
                for (int j = 0; j < InstantiatedPrefabs.Length; j++)
                {
                    poolingPrefabs[i].Prefabs[j] = InstantiatedPrefabs[j];
                }

                for (int j = InstantiatedPrefabs.Length; j < poolingPrefabs[i].Prefabs.Length; j++)
                {
                    poolingPrefabs[i].Prefabs[j] = Instantiate(poolingPrefabs[i].prefabSample);
                    poolingPrefabs[i].Prefabs[j].name = initialPrefabs[i].Prefab.name + j;
                    poolingPrefabs[i].Prefabs[j].SetActive(false);
                    poolingPrefabs[i].FreePrefabs.Add(j);
                }
            }
            int index = poolingPrefabs[i].FreePrefabs[0];
            poolingPrefabs[i].Prefabs[index].SetActive(true);
            poolingPrefabs[i].Prefabs[index].transform.SetPositionAndRotation(position, rotation);
            poolingPrefabs[i].Prefabs[index].transform.localScale =
                poolingPrefabs[i].prefabSample.transform.localScale;
            IPoolable[] prefabComponents = poolingPrefabs[i].Prefabs[index]
                .GetComponents<IPoolable>();
            for (int k = 0; k < prefabComponents.Length; k++)
            {
                prefabComponents[k].Inst();
            }

            poolingPrefabs[i].FreePrefabs.RemoveAt(0);
            return poolingPrefabs[i].Prefabs[index];
        }

        return null;
    }
    public void Destr(string PrefabName, string PrefabIndex)
    {
        for (int i = 0; i < poolingPrefabs.Length; i++)
        {
            if (poolingPrefabs[i].PrefabIndex != PrefabIndex) continue;
            
            for (int m = 0; m < poolingPrefabs[i].Prefabs.Length; m++)
            {
                if (poolingPrefabs[i].Prefabs[m].name != PrefabName) continue;
                
                Component[] prefabComponents = poolingPrefabs[i].Prefabs[m].GetComponents(typeof(IPoolable));
                for (int c = 0; c < prefabComponents.Length; c++)
                {
                    (prefabComponents[c] as IPoolable).DisPool();
                }
                poolingPrefabs[i].Prefabs[m].transform.parent = null;
                poolingPrefabs[i].Prefabs[m].SetActive(false);
                poolingPrefabs[i].FreePrefabs.Add(m);
                return;
            }
        }
    }
    public void Destr(GameObject obj)
    {
        for (int i = 0; i < poolingPrefabs.Length; i++)
        {
            for (int m = 0; m < poolingPrefabs[i].Prefabs.Length; m++)
            {
                if (!poolingPrefabs[i].Prefabs[m].name.Equals(obj.name)) continue;
                
                Component[] prefabComponents = poolingPrefabs[i].Prefabs[m].GetComponents(typeof(IPoolable));
                for (int c = 0; c < prefabComponents.Length; c++)
                {
                    (prefabComponents[c] as IPoolable).DisPool();
                }
                poolingPrefabs[i].Prefabs[m].transform.parent = null;
                poolingPrefabs[i].Prefabs[m].SetActive(false);
                poolingPrefabs[i].FreePrefabs.Add(m);
                return;
            }
        }
    }
    
    
}
[System.Serializable]
public struct PoolingPrefab
{
    public GameObject prefabSample;
    public string PrefabIndex;
    public GameObject[] Prefabs;
    public List<int> FreePrefabs;
    public PoolingPrefab(GameObject sample, string index, GameObject[] pr)
    {
        prefabSample = sample;
        PrefabIndex = index;
        Prefabs = pr;
        FreePrefabs = new List<int>();
        for (int i = 0; i < pr.Length; i++)
        {
            FreePrefabs.Add(i);
        }
    }
}

[System.Serializable]
public struct initialPrefab
{
    public GameObject Prefab;
    public string PrefabIndex;
    public int count;
}