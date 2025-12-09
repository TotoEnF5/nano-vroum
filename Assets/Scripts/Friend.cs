using System;
using UnityEngine;

public class Friend : MonoBehaviour
{
    public Transform character;
    
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        Vector3 force = character.position - transform.position;
        force *= 5;
        _rigidBody.AddForce(force);

        // Clamp velocity
        _rigidBody.linearVelocity =
            Vector2.ClampMagnitude(_rigidBody.linearVelocity, 6);
    }
}
