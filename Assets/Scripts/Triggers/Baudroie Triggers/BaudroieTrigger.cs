using System;
using UnityEngine;
using DG.Tweening;

public class BaudroieTrigger : MonoBehaviour
{
    [SerializeField]
    private bool showInGame = false;
    
    private void Awake()
    {
        BaudroieSourceTrigger source = transform.GetChild(0).GetComponent<BaudroieSourceTrigger>();
        source.showInGame = showInGame;
        
        for (int i = 1; i < transform.childCount; i++)
        {
            Transform destination = transform.GetChild(i);
            BaudroieDestinationTrigger trigger = destination.GetComponent<BaudroieDestinationTrigger>();
            source.Destinations.Add(new BaudroieSourceTrigger.Destination(destination, trigger.ease, trigger.time));
            trigger.showInGame = showInGame;
        }
    }
}
