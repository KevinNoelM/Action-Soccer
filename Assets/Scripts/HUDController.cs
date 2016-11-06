using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    protected Text scoreText, timeText; 

    private void Start()
    {
        GameManager.Instance.OnGoalScored += GoalScored;
    }

    private void GoalScored(int scoringTeam)
    {
        scoreText.text = string.Format("{0} - {1}", GameManager.Instance.Team0Score, GameManager.Instance.Team1Score);
    }

    private void Update()
    {
        var span = new TimeSpan(0, 0, (int)GameManager.Instance.GameCountDown);
        timeText.text = string.Format("{0}:{1:00}", (int)span.TotalMinutes, span.Seconds);
    }
}
