using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDifficulty : MonoBehaviour
{
    public static GameDifficulty instance;
    [SerializeField] private GameParameter[] parameters;

    public GameDifficulty()
    {
        instance = this;
    }
    public float getValue(string index)
    {
        for(int i = 0; i < parameters.Length;i++)
        {
            if (parameters[i].parameterIndex == index)
            {
                return parameters[i].parameterValues[LevelManager.instance.currentDifficulty];
            }
        }

        return 0;
    }
    public float getValue(string index, float defaultVal)
    {
        for(int i = 0; i < parameters.Length;i++)
        {
            if (parameters[i].parameterIndex == index)
            {
                return parameters[i].parameterValues[LevelManager.instance.currentDifficulty];
            }
        }

        return defaultVal;
    }
    
}
[System.Serializable]
public struct GameParameter
{
    public string parameterIndex;
    public float[] parameterValues;
}
