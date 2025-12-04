using System;
using UnityEngine;

public class Friend : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        Vector3 force = transform.parent.position - transform.position;
        force *= 5;
        _rigidBody.AddForce(force);

        // Clamp velocity
        _rigidBody.linearVelocity =
            Vector2.ClampMagnitude(_rigidBody.linearVelocity, 6);
    }
}
