using System;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {


    public Scoreboard scoreboard;
    public GameObject gameOverPanel;
    public Text gameOverText;

    public void UpdateScore(Team team) {
         scoreboard.SetScore(team.teamType, team.score);
    }

    public void GameOver(Team winningTeam) {
        gameOverPanel.SetActive(true);
        gameOverText.text = winningTeam.teamType.ToString() + " Team Wins!";
    }

    internal void ShowGameOverScreen(int playerScore, int opponentScore)
    {
        throw new NotImplementedException();

    }

    internal void UpdateOpponentScore(int opponentScore)
    {
        
    }

    internal void UpdatePlayerScore(int playerScore)
    {
        throw new NotImplementedException();
    }
}