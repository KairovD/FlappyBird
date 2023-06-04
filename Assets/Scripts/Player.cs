using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IGameLogic
{
    [SerializeField]private GameState myState;
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private float flyingForce;
    [SerializeField]private animationcontroller animController;
    [SerializeField]private Transform rotatingTr;
    public void Initialize()
    {
        animController.uptodate("Player");
    }

    public void changeState(GameState newState)
    {
        myState = newState;
        switch (myState)
        {
            case GameState.Play:
                rb.simulated = true;
                StartCoroutine(check());
                break;
            case GameState.Pause:
                rb.simulated = false;
                StopAllCoroutines();
                break;
        }
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

    public void FlyUp()
    {
        if (myState == GameState.Play)
        {
            rb.velocity = new Vector2(rb.velocity.x, flyingForce);
            //rb.AddRelativeForce(new Vector2(0, flyingForce));
            animController.PlayAnimation("Fly");
            audiodata.instance.playAudio("Fly");
        }
    }

    public IEnumerator check()
    {
        if (rb.velocity.y > 0)
        {
            //animController.playTransition("Up", 1);
            if(rotatingTr.eulerAngles.z < 50)
                rotatingTr.Rotate(0,0,2);
        }
        else
        {
            //animController.playTransition("Down", 0);
            if(rotatingTr.eulerAngles.z > 10)
                rotatingTr.Rotate(0,0,-1);
        }
        yield return null;
        if (myState == GameState.Play)
            StartCoroutine(check());
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            LevelManager.instance.PlayerLose();
            audiodata.instance.playAudio("Hit");
        }
    }    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PassageExit"))
        {
            LevelManager.instance.addScore();
            audiodata.instance.playAudio("Score");
        }
    }
}
