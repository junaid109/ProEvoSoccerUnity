using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour {
    public string name;
    public int jerseyNumber;
    public float speed;
    public float shotPower;
    public float passing;
    public float dribbling;
    public float defending;
    public float physicality;
    public Vector3 position;
    public bool isGoalkeeper;

      public string playerName;
    public int shirtNumber;
    public float moveSpeed = 5.0f;
    public float kickForce = 10.0f;
    public Team team;

    private Rigidbody _rb;
    private bool _hasBall;


    public Player(string name, int jerseyNumber, float speed, float shotPower, float passing, float dribbling, float defending, float physicality, Vector3 position, bool isGoalkeeper) {
        this.name = name;
        this.jerseyNumber = jerseyNumber;
        this.speed = speed;
        this.shotPower = shotPower;
        this.passing = passing;
        this.dribbling = dribbling;
        this.defending = defending;
        this.physicality = physicality;
        this.position = position;
        this.isGoalkeeper = isGoalkeeper;
    }

      private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        // Move the player in the specified direction
        _rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
    }

    public void Pass(Vector3 direction)
    {
        if (_hasBall)
        {
            // Pass the ball in the specified direction
            team.ball.transform.position = transform.position + direction.normalized * 2.0f;
            team.ball.Kick(direction.normalized * kickForce);

            // Player no longer has the ball
            _hasBall = false;
        }
    }

    public void Shoot(Vector3 direction)
    {
        if (_hasBall)
        {
            // Shoot the ball in the specified direction
            team.ball.transform.position = transform.position + direction.normalized * 2.0f;
            team.ball.Kick(direction.normalized * kickForce * 2.0f);

            // Player no longer has the ball
            _hasBall = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the player collides with the ball, they now have possession
        if (collision.gameObject.CompareTag("Ball"))
        {
            _hasBall = true;
        }
    }
}