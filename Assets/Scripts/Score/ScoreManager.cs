using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int Score { get; private set; }

    public static event Action<int> OnScoreChanged;

    void Awake()
    {
        if ((Instance != this && Instance != null) || FindObjectsOfType<ScoreManager>().Length > 1)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    private void Start()
    {
        DataManager.OnDataLoaded += GetLatestSavedScoreHook;
    }

    // Score Manager
    public void AddScore(int num)
    {
        Score += num;
        OnScoreChanged?.Invoke(Score);
    }


    private void GetLatestSavedScoreHook()
    {
        if (DataManager.userProfile == null)
        {
            return;
        }

        this.Score = DataManager.userProfile.savedScore;

        // return DataManager.userProfile.savedScore;
    }

}
