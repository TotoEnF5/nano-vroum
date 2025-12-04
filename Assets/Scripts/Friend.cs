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
        force *= 3;
        _rigidBody.AddForce(force);
    }
}
