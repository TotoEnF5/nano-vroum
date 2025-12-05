using System;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [HideInInspector] public Baudroie baudroie;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        baudroie.DoAnimation();
    }
}
