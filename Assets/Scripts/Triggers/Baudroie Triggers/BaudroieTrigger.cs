using System;
using UnityEngine;

public class BaudroieTrigger : MonoBehaviour
{
    public MoveBaudroie baudroie;
    public bool doSomething = true;
    public bool showInGame = false;
    public float time = 2;
    public MoveBaudroie.BaudroieDestination destination = MoveBaudroie.BaudroieDestination.Custom;
    public Transform triggerDestination;
    public Vector3 customPosition;
    public bool oneShot = true;

    private bool _triggered = false;

    private void Awake()
    {
        GetComponent<Renderer>().enabled = showInGame;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!doSomething || _triggered || !other.CompareTag("Baudroie"))
        {
            return;
        }

        if (destination == MoveBaudroie.BaudroieDestination.BaudroieTrigger)
        {
            baudroie.StartMovement(triggerDestination, time);
        }
        else if (destination == MoveBaudroie.BaudroieDestination.Custom)
        {
            baudroie.StartMovement(customPosition, time);
        }

        _triggered = oneShot;
    }
}
