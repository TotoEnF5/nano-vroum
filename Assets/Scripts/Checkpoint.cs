using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool _triggered = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_triggered || !(other.CompareTag("target") || other.CompareTag("Player")))
        {
            return;
        }
        
        GamestateManager.Instance.SetCheckpoint(transform);
        _triggered = true;
    }
}
