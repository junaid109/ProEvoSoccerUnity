using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float kickForce = 1000.0f;
    public Rigidbody rigidBody;
    public float maxSpeed = 20.0f;
    public float drag = 0.5f;
    private Vector3 _movement;

    private Rigidbody _rb;
    private AudioSource _audioSource;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Kick(Vector3 direction)
    {
        // Apply force to the ball in the specified direction
        _rb.AddForce(direction * kickForce, ForceMode.Impulse);

        // Play kick sound effect
        _audioSource.Play();
    }

    private void FixedUpdate()
    {
        // Apply drag to the ball to slow it down over time
        _rb.velocity -= _rb.velocity * drag;
        _rb.angularVelocity -= _rb.angularVelocity * drag;

        // Clamp the ball's velocity to the maximum speed
        if (_rb.velocity.magnitude > maxSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * maxSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _movement = Vector3.Reflect(_movement, collision.contacts[0].normal);
        // Play collision sound effect
        _audioSource.Play();
    }

     public void SetMovement(Vector3 movement) {
        _movement = movement.normalized;
    }

    internal void StartMoving()
    {
        throw new NotImplementedException();
    }

    internal void StopMoving()
    {
        throw new NotImplementedException();
    }

    internal void Reset()
    {
        throw new NotImplementedException();
    }
}
