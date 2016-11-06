using UnityEngine;
using System.Collections;
using System;

public class TeamManager : MonoBehaviour
{
    [Serializable]
    public struct TeamColours
    {
        public Color Hair;
        public Color Kit;
        public Color Skin;
        public Color SkinExtra;
    }

    [SerializeField]
    protected int teamId;

    [SerializeField]
    protected FootballerMovement[] players;

    [SerializeField]
    protected TeamColours teamColours;

    private FootballerMovement currentlySelectedPlayer;
    private int numberOfPlayers;

    private float autoSwitchTime = 1.0f;
    private float autoSwitchCounter = 1.0f;
    private bool manualSwitchCooldownOver = true;
    [SerializeField]
    protected float manualSwitchCooldownTime = 5.0f;
    private float manualSwitchCooldownCounter = 0.0f;

    private static KeyCode team1Switch = KeyCode.Q;
    private static KeyCode team2Switch = KeyCode.Keypad4;

    public FootballerMovement CurrentlySelectedPlayer
    {
        get { return currentlySelectedPlayer; }
    }

    private Transform ball;

    public FootballerMovement GetNextClosestPlayer()
    {
        int index = currentlySelectedPlayer == GetClosestPlayer() ? 1 : 0;
        return players[index];
    }

    private FootballerMovement GetClosestPlayer()
    {
        float[] keys = new float[numberOfPlayers];
        for (int i = 0; i < numberOfPlayers; i++)
        {
            keys[i] = Vector3.Distance(ball.position, players[i].chestBody.position);
        }
        Array.Sort(keys, players);
        return players[0];
    }

    private void Start()
    {
        numberOfPlayers = players.Length;
        for (int i = 0; i < numberOfPlayers; i++)
        {
            players[i].GetComponent<ColourSetter>().SetUp(teamColours.Hair, teamColours.Kit, teamColours.Skin, teamColours.SkinExtra);
        }

        ball = GameManager.Instance.Ball.transform;
        SetSelectedPlayer(players[0]);
    }

    private void SetSelectedPlayer(FootballerMovement player)
    {
        if (currentlySelectedPlayer != null)
        {
            currentlySelectedPlayer.ToggleIndicator(false);
        }
        currentlySelectedPlayer = player;
        player.ToggleIndicator(true);
    }

    private void Update()
    {
        if (teamId == 0 && Input.GetKeyDown(team1Switch) || teamId == 1 && Input.GetKeyDown(team2Switch))
        {
            SetSelectedPlayer(GetNextClosestPlayer());
            manualSwitchCooldownOver = false;
            manualSwitchCooldownCounter = manualSwitchCooldownTime;
        }

        CheckForAutoSwitch();
        ManualSwitchCooldown();
    }

    private void CheckForAutoSwitch()
    {
        if (currentlySelectedPlayer != null && !currentlySelectedPlayer.IsInPossession && manualSwitchCooldownOver)
        {
            autoSwitchCounter += Time.deltaTime;
            if (autoSwitchCounter >= autoSwitchTime)
            {
                autoSwitchCounter -= autoSwitchTime;
                FootballerMovement player = GetClosestPlayer();
                if (player != currentlySelectedPlayer)
                {
                    SetSelectedPlayer(player);
                }
            }
        }
    }

    private void ManualSwitchCooldown()
    {
        if (!manualSwitchCooldownOver)
        {
            manualSwitchCooldownCounter -= Time.deltaTime;
            if (manualSwitchCooldownCounter <= 0)
            {
                manualSwitchCooldownOver = true;
            }
        }
    }
}
