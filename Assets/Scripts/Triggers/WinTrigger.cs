using System;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public bool showInGame = false;
    
    private void Awake()
    {
        GetComponent<Renderer>().enabled = showInGame;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        
        GamestateManager.Instance.SetGamestate(Gamestate.Win);
    }
}
