using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LootLocker.Requests;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


[System.Serializable]
public class ScoreId
{
    public string name;
    public int id;
}

public class MainMenuScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public ScoreId[] onlineScores = new ScoreId[3]; 
    public StoreScoreObj scoreObj;
    
    
    public TextMeshProUGUI localScoreTxt;
    public TextMeshProUGUI onlineScoreTxt;

    private void Start()
    {
        scoreObj.Load();

        localScoreTxt.text = $"Tutorial: {scoreObj.highScores[0].ToString()}\n" +
                             $"Forest: {scoreObj.highScores[1].ToString()}\n" +
                             $"Cave: {scoreObj.highScores[2].ToString()}";
        
        StartSession();
    }
    
    private void StartSession()
    {
        LootLockerSDKManager.StartSession("Player", (response) =>
        {
            Debug.Log(response.success ? "Login success" : $"Login Error: {response.Error}");
        });
    }

    public void ShowOnlineScores()
    {
        string msg = "";
        bool fetchedResults = false;
        foreach (var t in onlineScores)
        {
            LootLockerSDKManager.GetScoreList(t.id, 1, (response) =>
            {
                if (response.success)
                {
                    msg += $"{t.name}: {(double)response.items[0].score/100} ({response.items[0].member_id})\n";
                    fetchedResults = true;
                    onlineScoreTxt.text = fetchedResults ? msg : "Failed to get Online Scores";
                }
                else
                {
                    Debug.Log($"Get Score {t.id} Failed: {response.Error}");
                }
                    
            });
        }

        

    }

    public void SubmitScores()
    {
        int tutorial = (int)(scoreObj.highScores[0] * 100);
        int forest =  (int)(scoreObj.highScores[1] * 100);
        int cave =  (int)(scoreObj.highScores[2] * 100);

        if (tutorial > 0) LootLockerSDKManager.SubmitScore(playerName.text, (int)tutorial, onlineScores[0].id, (response) =>
        {
            Debug.Log(response.success ? $"Submit score success" : $"Submit score Failed {onlineScores[0].name}: {response.Error}");
        });
        
        if (forest > 0) LootLockerSDKManager.SubmitScore(playerName.text, (int)forest, onlineScores[1].id, (response) =>
        {
            Debug.Log(response.success ? $"Submit score success" : $"Submit score Failed {onlineScores[1].name}: {response.Error}");
        });
        
        if (cave > 0) LootLockerSDKManager.SubmitScore(playerName.text, (int)cave, onlineScores[2].id, (response) =>
        {
            Debug.Log(response.success ? $"Submit score success" : $"Submit score Failed {onlineScores[2].name}: {response.Error}");
        });
    }
}
