using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool showInGame = false;
    
    private bool _triggered = false;
    
    private void Awake()
    {
        GetComponent<Renderer>().enabled = showInGame;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_triggered || !other.CompareTag("Player"))
        {
            return;
        }
        
        GamestateManager.Instance.SetCheckpoint(transform);
        _triggered = true;
    }
}
