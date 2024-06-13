using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        ScoreManager.OnScoreChanged += ChangeScoreText;
    }

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = ScoreManager.Instance.Score.ToString("D3");
    }

    private void OnDestroy()
    {
        ScoreManager.OnScoreChanged -= ChangeScoreText;
    }

    private void ChangeScoreText(int addedScore)
    {
        scoreText.text = addedScore.ToString("D3");
    }
}
