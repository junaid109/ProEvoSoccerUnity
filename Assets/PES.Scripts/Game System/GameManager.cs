using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public UIManager uIManager;
    public static GameManager instance;

    public enum GameState { Idle, Playing, Paused, GameOver };
    public GameState gameState = GameState.Idle;

    public GameObject ballPrefab;
    public GameObject playerPrefab;
    public GameObject opponentPrefab;

    public Transform ballSpawnPoint;
    public Transform playerSpawnPoint;
    public Transform opponentSpawnPoint;

    private GameObject ball;
    private GameObject player;
    private GameObject opponent;

    private int playerScore = 0;
    private int opponentScore = 0;

    public int maxScore = 3;

    // create method to reset game timer
    


    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Start () {
        SpawnBall();
        SpawnPlayers();
        StartGame();
    }

    void Update () {
        if (gameState == GameState.Playing) {
            CheckScore();
        }
    }

    void SpawnBall() {
        ball = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity) as GameObject;
    }

    void SpawnPlayers() {
        player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity) as GameObject;
        opponent = Instantiate(opponentPrefab, opponentSpawnPoint.position, Quaternion.identity) as GameObject;
    }

    void StartGame() {
        gameState = GameState.Playing;
        ball.GetComponent<Ball>().StartMoving();
    }

    void PauseGame() {
        gameState = GameState.Paused;
        ball.GetComponent<Ball>().StopMoving();
    }

    void ResumeGame() {
        gameState = GameState.Playing;
        ball.GetComponent<Ball>().StartMoving();
    }

    void EndGame() {
        gameState = GameState.GameOver;
        ball.GetComponent<Ball>().StopMoving();
        uIManager.ShowGameOverScreen(playerScore, opponentScore);
    }

    void CheckScore() {
        if (ball.transform.position.x > playerSpawnPoint.position.x) {
            opponentScore++;
            uIManager.UpdateOpponentScore(opponentScore);
            if (opponentScore >= maxScore) {
                EndGame();
            } else {
                ball.GetComponent<Ball>().Reset();
            }
        } else if (ball.transform.position.x < opponentSpawnPoint.position.x) {
            playerScore++;
            uIManager.UpdatePlayerScore(playerScore);
            if (playerScore >= maxScore) {
                EndGame();
            } else {
                ball.GetComponent<Ball>().Reset();
            }
        }
    }

    public void PlayerKick() {
        ball.GetComponent<Ball>().Kick(player.transform.position);
    }

    public void OpponentKick() {
        ball.GetComponent<Ball>().Kick(opponent.transform.position);
    }

    internal void FoulCommitted(Player player, Team opposingTeam)
    {
        throw new NotImplementedException();
    }

    internal void OutOfBounds(Ball ball)
    {
        throw new NotImplementedException();
    }

    internal void UpdateScore(Team team)
    {
        throw new NotImplementedException();
    }
} 