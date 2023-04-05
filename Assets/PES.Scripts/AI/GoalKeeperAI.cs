using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeperAI : MonoBehaviour
{
    public Transform ballTransform;
    public Transform goalTransform;
    public float moveSpeed = 5.0f;
    public float jumpChance = 0.2f;
    public float diveChance = 0.2f;

    public Animator _animator;

    private Vector3 _goalCenter;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _goalCenter = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards the ball if it's close enough
        if (Vector3.Distance(transform.position, ballTransform.position) < 10.0f) {
            Vector3 direction = (ballTransform.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else {
            // Move back towards the center of the goal
            Vector3 direction = (goalTransform.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }

          float distanceToBall = Vector3.Distance(transform.position, ball.position);

        // If the ball is close enough to the goal, the AI goalkeeper has a chance to jump or dive
        if (distanceToBall < 10f)
        {
            float jumpRandom = Random.Range(0f, 1f);
            float diveRandom = Random.Range(0f, 1f);

            if (jumpRandom <= jumpChance)
            {
                Jump();
            }
            else if (diveRandom <= diveChance)
            {
                Dive();
            }
            else
            {
                // If the AI goalkeeper doesn't jump or dive, they will move towards the ball to block it
                Vector3 targetPosition = ball.position - transform.forward * 2f;
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
            }
        }
        else
        {
            // If the ball is not close enough to the goal, the AI goalkeeper will return to the goal center
            transform.position = Vector3.Lerp(transform.position, _goalCenter, Time.deltaTime);
        }
    }

    
    void Jump()
    {
        StartCoroutine("JumpCoroutine");
    }

    void Dive()
    {
        StartCoroutine("DiveCoroutine");
    }

      private IEnumerator JumpCoroutine(float targetHeight)
    {
        float startHeight = transform.position.y;
        float jumpDuration = Mathf.Sqrt((targetHeight - startHeight) * 2 / Physics.gravity.magnitude);
        float jumpStartTime = Time.time;

        while (Time.time < jumpStartTime + jumpDuration)
        {
            float t = (Time.time - jumpStartTime) / jumpDuration;
            float height = Mathf.Lerp(startHeight, targetHeight, t);
            Vector3 position = transform.position;
            position.y = height;
            transform.position = position;

            yield return null;
        }
    }

    private IEnumerator DiveCoroutine(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float diveDuration = Vector3.Distance(startPosition, targetPosition) / diveSpeed;
        float diveStartTime = Time.time;

        while (Time.time < diveStartTime + diveDuration)
        {
            float t = (Time.time - diveStartTime) / diveDuration;
            Vector3 position = Vector3.Lerp(startPosition, targetPosition, t);
            position.y += diveHeight * (1.0f - Mathf.Abs(2.0f * t - 1.0f));
            transform.position = position;

            yield return null;
        }
    }
}
