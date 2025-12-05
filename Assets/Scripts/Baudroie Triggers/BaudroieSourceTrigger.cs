using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BaudroieSourceTrigger : MonoBehaviour
{
    public struct Destination
    {
        public Destination(Transform goal, Ease ease, float time)
        {
            Goal = goal;
            Ease = ease;
            Time = time;
        }
        
        public readonly Transform Goal;
        public readonly Ease Ease;
        public readonly float Time;
    }

    [HideInInspector] public bool showInGame;
    [HideInInspector] public List<Destination> Destinations = new List<Destination>();

    private bool _triggered = false;

    private void Awake()
    {
        GetComponent<Renderer>().enabled = showInGame;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_triggered && !other.CompareTag("Baudroie"))
        {
            return;
        }

        Sequence sequence = DOTween.Sequence();
        foreach (Destination destination in Destinations)
        {
            sequence.Append(other.transform.DOMove(destination.Goal.position, destination.Time).SetEase(destination.Ease));
        }
        
        _triggered = true;
    }
}
