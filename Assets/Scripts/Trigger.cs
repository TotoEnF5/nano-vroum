using UnityEngine;

public class Trigger : MonoBehaviour
{
    
    [SerializeField] private MoveCamera cameraScript;
    [SerializeField] private bool showInGame = false;
    [SerializeField] private float time = 2;
    [SerializeField] private MoveCamera.CameraDestination cameraDestination;

    private void Awake()
    {
        GetComponent<Renderer>().enabled = showInGame;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cameraScript.StartMovement(transform, time, cameraDestination);
        }
    }
}
