using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int record;
    public int currentDifficulty;
    public Transform playerPos;
    public InputController inputManager;
    public UnityEvent LoseScreenApper;
    public UnityEvent onNewRecord;
    private int currentScore;
    public TextMeshProUGUI[] scoreVisualisations;
    public LevelManager()
    {
        instance = this;
    }

    private void Awake()
    {
        currentDifficulty = PlayerPrefs.GetInt("Difficulty", 0);
        record = PlayerPrefs.GetInt("Record" + currentDifficulty,-1);
        GameObject player = PoolingManager.instance.Inst("Player", playerPos.position, playerPos.rotation);
        player.GetComponent<Player>().Initialize();
        inputManager.PlayerScript = player.GetComponent<Player>();
        Pause();
    }

    public void PlayerLose()
    {
        GameStateManager.instance.setState(GameState.Pause);
        if (currentScore > PlayerPrefs.GetInt("Record" + currentDifficulty, -1))
        {
            PlayerPrefs.SetInt("Record" + currentDifficulty, currentScore);
            StartCoroutine(lose(true));
        }
        else
        {
            StartCoroutine(lose(false));
        }
    }

    IEnumerator lose(bool newRecord)
    {
        yield return new WaitForSeconds(0.5f);
        LoseScreenApper.Invoke();
        if(newRecord)
            onNewRecord.Invoke();
    }

    public void Restart()
    {
        SceneLoader.instance.LoadScene("Game");
    }

    public void MainMenu()
    {
        SceneLoader.instance.LoadScene("Menu");
    }

    public void Pause()
    {
        GameStateManager.instance.setState(GameState.Pause);
    }

    public void Resume()
    {
        GameStateManager.instance.setState(GameState.Play);
    }

    public void addScore()
    {
        currentScore++;
        for (int i = 0; i < scoreVisualisations.Length; i++)
        {
            scoreVisualisations[i].text = currentScore.ToString();
        }
    }

    public void Menu()
    {
        SceneLoader.instance.LoadScene("Menu");
    }
}
