using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeper : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float tackleRange = 1.5f;
    public float diveDistance = 3.0f;
    public float diveDuration = 0.5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check for input to move the goal keeper
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;

        // Move the goal keeper
        rb.MovePosition(transform.position + movement);

        // Check for input to make a tackle
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Tackle();
        }

        // Check for input to make a dive
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dive();
        }
    }

    private void Tackle()
    {
        // Check if there is a player within tackle range
        Collider[] colliders = Physics.OverlapSphere(transform.position, tackleRange);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // Move the goal keeper towards the player and stop
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                rb.MovePosition(transform.position + direction * tackleRange);
                return;
            }
        }
    }

    private void Dive()
    {
        // Check if there is a ball within dive distance
        GameObject ball = GameManager.instance.ball;
        float distance = Vector3.Distance(transform.position, ball.transform.position);

        if (distance <= diveDistance)
        {
            // Move the goal keeper towards the ball and rotate towards it
            Vector3 direction = (ball.transform.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * diveDistance);

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(targetRotation);

            // Wait for the dive duration and then reset the rotation
            StartCoroutine(ResetRotationAfterDelay(diveDuration));
        }
    }

    private IEnumerator ResetRotationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.MoveRotation(Quaternion.identity);
    }
}
