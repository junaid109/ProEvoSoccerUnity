using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
   public Team team;
    public float moveSpeed;
    public float kickPower;
    public float maxDistanceToBall;
    public float minPassDistance;
    public float timeBetweenDecisions;

    private Rigidbody rb;
    private bool hasBall;
    private float decisionTimeCounter;
    private List<Player> teammates;
    private List<Player> opponents;
    private Ball ball;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        teammates = team.GetPlayers();
        opponents = team.GetOpponents().GetPlayers();
        ball = GameManager.instance.ball;
    }

private void Update()
    {
        decisionTimeCounter -= Time.deltaTime;
        if (decisionTimeCounter <= 0f)
        {
            MakeDecision();
            decisionTimeCounter = timeBetweenDecisions;
        }
    }

    private void MakeDecision()
    {
        if (hasBall)
        {
            PassBall();
        }
        else
        {
            MoveTowardsBall();
        }
    }

    private void MoveTowardsBall()
    {
        if (ball == null)
        {
            return;
        }

        if (Vector3.Distance(transform.position, ball.transform.position) <= maxDistanceToBall)
        {
            Vector3 direction = (ball.transform.position - transform.position).normalized;
            rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);
        }
    }

    private void PassBall()
    {
        Player teammateToPassTo = null;

        float minDistanceToTeammate = float.MaxValue;
        foreach (Player teammate in teammates)
        {
            float distanceToTeammate = Vector3.Distance(transform.position, teammate.transform.position);
            if (distanceToTeammate < minPassDistance && distanceToTeammate < minDistanceToTeammate)
            {
                minDistanceToTeammate = distanceToTeammate;
                teammateToPassTo = teammate;
            }
        }

        if (teammateToPassTo != null)
        {
            Vector3 passDirection = (teammateToPassTo.transform.position - transform.position).normalized;
            ball.SetVelocity(passDirection * kickPower);
            hasBall = false;
        }
    }

        private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            hasBall = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            hasBall = false;
        }
    }
}
