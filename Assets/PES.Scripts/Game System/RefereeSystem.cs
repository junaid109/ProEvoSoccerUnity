using UnityEngine;
using UnityEngine.UI;

public class RefereeSystem : MonoBehaviour {
    public Team team1;
    public Team team2;
    public Ball ball;
    public GameManager gameManager;

    private void OnCollisionEnter(Collision collision) {
        // Check for fouls or other infractions
        if (collision.gameObject.CompareTag("Player")) {
            var player = collision.gameObject.GetComponent<Player>();
            var opposingTeam = player.team == team1 ? team2 : team1;
            if (opposingTeam.HasPossession() && player.IsTackling()) {
                // Foul committed
                gameManager.FoulCommitted(player, opposingTeam);
            }
        }
        else if (collision.gameObject.CompareTag("Ball")) {
            // Check for out of bounds
            if (!IsInPlay()) {
                gameManager.OutOfBounds(ball);
            }
        }
    }

    public bool IsInPlay() {
        return true; // TODO: Implement this method to check if the ball is in play
    }
}