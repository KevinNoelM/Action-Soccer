using UnityEngine;
using System.Collections;

public class FootballerMovement : CharacterMovement
{
    [SerializeField]
    protected float kickForce = 50;
    [SerializeField]
    protected int teamId;
    [SerializeField]
    protected GameObject indicator;
    [SerializeField]
    protected PlayerPositionData playerPositionData;

    public bool IsInPossession
    {
        get { return possessionController.PlayerHasPossession; }
    }

    private TeamManager team;
    private Vector3 startingPosition;
    private PlayerPossessionController possessionController;

    public void ToggleIndicator(bool active)
    {
        indicator.SetActive(active);
    }

    protected override void Start()
    {
        base.Start();
        possessionController = GetComponentInChildren<PlayerPossessionController>();
        team = GameManager.Instance.Teams[teamId];

        float startingX = GameManager.Instance.FieldMarkerInfo.bottomLeft.position.x + GameManager.Instance.FieldMarkerInfo.Length * playerPositionData.MinX;
        float startingZ = GameManager.Instance.FieldMarkerInfo.bottomLeft.position.z + GameManager.Instance.FieldMarkerInfo.Width * playerPositionData.MinZ;

        int switchSide = teamId == 0 ? 1 : -1;
        startingPosition = new Vector3(startingX * switchSide, 0, switchSide * startingZ);
    }

    protected override void Update()
    {
        base.Update();
        if (possessionController.PlayerHasPossession)
        {
            if (input.PressKick())
            {
                Rigidbody ball = possessionController.Ball;
                Vector3 kick = (ball.transform.position - chestBody.transform.position) * kickForce;
                kick.y = 0.0f;
                ball.AddForce(kick, ForceMode.Impulse);
            }
            else if (input.PressJump())
            {
                Rigidbody ball = possessionController.Ball;
                Vector3 kick = (ball.transform.position - chestBody.transform.position) * kickForce;
                kick.y = kickForce * 0.5f;
                ball.AddForce(kick, ForceMode.Impulse);
            }
        }
    }

    protected override bool IsPlayerControlled()
    {
        return team.CurrentlySelectedPlayer == this;
    }

    protected override Vector3 OutOfPossessionDirection()
    {
        Vector3 target = OutOfPossessionTargetPosition();
        Vector3 directionToTarget = target - chestBody.transform.position;
        return directionToTarget.normalized;
    }

    private Vector3 OutOfPossessionTargetPosition()
    {
        float ballX = teamId == 0 ? GameManager.Instance.BallXPosition : 1 - GameManager.Instance.BallXPosition;
        float ballZ = teamId == 0 ? GameManager.Instance.BallZPosition : 1 - GameManager.Instance.BallZPosition;
        int switchSide = teamId == 0 ? 1 : -1;
        float newX = startingPosition.x + switchSide * (GameManager.Instance.FieldMarkerInfo.Length * playerPositionData.XLength * playerPositionData.XCurveAt(ballX));
        float newZ = startingPosition.z + switchSide * (GameManager.Instance.FieldMarkerInfo.Width * playerPositionData.ZLength * playerPositionData.ZCurveAt(ballZ));
        Vector3 target = chestBody.transform.position;
        target.x = newX;
        target.z = newZ;
        return target;
    }
}
