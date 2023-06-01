using UnityEngine;
using TMPro;
using System;

public class ScoreUIController: MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTextUI;
    [SerializeField] Wall goalPost;

    private void OnEnable()
    {
        Wall.OnBallGoalEvent += UpdateUI;
    }

    private void UpdateUI()
    {
        UpdateScoreTextUI(goalPost.GetCurrentScore());
    }

    public void ResetUI()
    {
        UpdateScoreTextUI(0);
    }


    private void UpdateScoreTextUI(int score)
    {
        scoreTextUI.text = score.ToString();
    }

    
}
