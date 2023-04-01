using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour {
    public Team team;
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ball")) {
            gameManager.UpdateScore(team);
        }
    }
}