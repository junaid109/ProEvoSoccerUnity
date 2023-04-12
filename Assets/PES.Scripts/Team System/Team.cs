using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team {
    public TeamType teamType;
    public string teamName;
    public Player[] players;
    public Ball ball;
    public int score;

    internal bool HasPossession()
    {
        throw new NotImplementedException();
    }

    private void Start()
    {
        // Initialize all players
        foreach (var player in players)
        {
            player.team = this;
        }

        // Initialize the ball
        ball = GameObject.FindObjectOfType<Ball>();
    }
}