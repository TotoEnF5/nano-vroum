using System;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        
        GamestateManager.Instance.SetGamestate(Gamestate.Win);
    }
}
