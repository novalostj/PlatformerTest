using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public StoreScoreObj score;
    
    public void Exit()
    {
        Application.Quit();
    }

    public void StartGame(int i)
    {
        SceneManager.LoadScene(i, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    private void Start()
    {
        score.Load();
    }

    private void OnDestroy()
    {
        score.Save();
    }

    public void SetScore(double gameScore)
    {
        if (score.highScores[SceneManager.GetActiveScene().buildIndex - 1] > gameScore || score.highScores[SceneManager.GetActiveScene().buildIndex - 1] == 0) score.highScores[SceneManager.GetActiveScene().buildIndex - 1] = gameScore;
    }

    public double GetHighScore()
    {
        return score.highScores[SceneManager.GetActiveScene().buildIndex - 1];
    }
}
