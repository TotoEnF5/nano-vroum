using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    private MoveCamera cameraScript;
    
    [SerializeField]
    private float time = 0.5f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cameraScript.StartMovement(transform.position, time);
        }
    }
}
