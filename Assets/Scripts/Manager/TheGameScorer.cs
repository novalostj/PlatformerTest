using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class TheGameScorer : MonoBehaviour
{
    public MainMenuManager manager;
    public Movement player;
    public TextMeshProUGUI score;
    public GameObject finishUI;
    public TextMeshProUGUI onFinishedScore;
    public TextMeshProUGUI highScore;
    
    public float runTimer;
    
    public bool isGameRunning;

    private void Start()
    {
        finishUI.SetActive(false);
    }

    private void Update()
    {
        runTimer += isGameRunning ? Time.deltaTime : 0;
        score.text = Math.Round(runTimer, 2) + "s";
    }

    private void OnEnable()
    {
        Lever.set += Setter;
    }

    private void OnDisable()
    {
        Lever.set -= Setter;
    }

    private void Setter()
    {
        isGameRunning = !isGameRunning;

        if (!isGameRunning)
        {
            player.canMove = false;
            finishUI.SetActive(true);
            onFinishedScore.text = $"SCORE: {score.text}";
            highScore.text = $"H_SCORE: {manager.GetHighScore()}";
            manager.SetScore(Math.Round(runTimer, 2));
        }
            
    }
}