using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public List<IGameLogic> gameLogicElements = new List<IGameLogic>();

    [SerializeField]private GameState currentGameState;
    //we have to manually make sure it is the only instance in current scene

    public GameStateManager()
    {
        instance = this;
    }

    public GameState getState()
    {
        return currentGameState;
    }

    public void setState(GameState newState)
    {
        currentGameState = newState;
        for (int i = 0; i < gameLogicElements.Count; i++)
        {
            gameLogicElements[i].changeState(newState);
        }
    }
}
