using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour, IGameLogic
{
    private GameState myState;
    [Tooltip("Must Be Normalized")][SerializeField]private Vector2 myMoveVector;
    private Vector2 actualMoveVector;
    [SerializeField]private Rigidbody2D rb;

    public void initialize(float movingSpeed)
    {
        actualMoveVector = myMoveVector * movingSpeed;
    }
    public void initialize(float movingSpeed, float traversedDist)
    {
        actualMoveVector = myMoveVector * movingSpeed;
        transform.position = transform.position + (Vector3)(myMoveVector * traversedDist);
    }
    public void changeState(GameState newState)
    {
        myState = newState;
    }

    public void DisPool()
    {
        GameStateManager.instance.gameLogicElements.Remove(this);
        changeState(GameState.Pause);
    }

    public void Inst()
    {
        GameStateManager.instance.gameLogicElements.Add(this);
        changeState(GameStateManager.instance.getState());
    }

    public void FixedUpdate()
    {
        if(myState == GameState.Play)
            rb.MovePosition(rb.position + actualMoveVector);
    }
}
