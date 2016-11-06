using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public delegate void GoalScoredEvent(int teamId);
    public GoalScoredEvent OnGoalScored;
        

    [System.Serializable]
    public class FieldMarkers
    {
        public Transform bottomLeft, topLeft, bottomRight, topRight, bottomCenter, topCenter, ballStart;
        private float length, width;
        public float Length
        {
            get { return length; }
        }
        public float Width
        {
            get { return width; }
        }

        public void SetUp()
        {
            width = Vector3.Distance(bottomLeft.position, topLeft.position);
            length = Vector3.Distance(bottomLeft.position, bottomRight.position);
        }

    }

    public static GameManager Instance;

    [SerializeField]
    protected TeamManager[] teams;

    public TeamManager[] Teams
    {
        get { return teams; }
    }

    [SerializeField]
    protected GameObject ball;

    public GameObject Ball
    {
        get { return ball; }
    }

    [SerializeField]
    protected FieldMarkers fieldMarkers;
    public FieldMarkers FieldMarkerInfo
    {
        get { return fieldMarkers; }
    }

    private float ballXPosition;
    public float BallXPosition
    {
        get { return ballXPosition; }
    }

    private float ballZPosition;
    public float BallZPosition
    {
        get { return ballZPosition; }
    }

    [SerializeField]
    protected int gameLength = 240;
    private float gameCountDown;
    public float GameCountDown
    {
        get { return gameCountDown; }
    }

    private int team0score = 0, team1score = 0;
    public int Team0Score
    {
        get { return team0score; }
    }
    public int Team1Score
    {
        get { return team1score; }
    }

    public void CalculateBallXPosition()
    {
        ballXPosition = (ball.transform.position.x - fieldMarkers.bottomLeft.position.x) / fieldMarkers.Length;
    }

    public void CalculateBallZPosition()
    {
        ballZPosition = (ball.transform.position.z - fieldMarkers.bottomLeft.position.z) / fieldMarkers.Width;
    }


    public void GoalScored(int scoringTeam)
    {
        ball.GetComponent<Rigidbody>().MovePosition(fieldMarkers.ballStart.position);
        switch(scoringTeam)
        {
            case 0: team0score++;
                break;
            case 1: team1score++;
                    break;
        }
        if (OnGoalScored != null)
        {
            OnGoalScored(scoringTeam);
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        fieldMarkers.SetUp();
        gameCountDown = gameLength;
    }

    private void Update()
    {
        if (gameCountDown <= 0)
        {
            //gameover
        }
        else
        {
            CalculateBallXPosition();
            CalculateBallZPosition();
            gameCountDown -= Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
