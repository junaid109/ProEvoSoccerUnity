using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {
    public Text homeScoreText;
    public Text awayScoreText;

    private int _homeScore = 0;
    private int _awayScore = 0;

    public void SetScore(TeamType team, int score) {
        if (team == TeamType.Home) {
            _homeScore = score;
            homeScoreText.text = _homeScore.ToString();
        } else {
            _awayScore = score;
            awayScoreText.text = _awayScore.ToString();
        }
    }
}