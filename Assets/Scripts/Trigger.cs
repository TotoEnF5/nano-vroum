using UnityEngine;

public class Trigger : MonoBehaviour
{
    /**
     * Where the camera should end up relative to the trigger.
     */
    public enum CameraDestination
    {
        Top,    // The camera goes at the top of the trigger
        Bottom, // The camera goes at the bottom of the trigger
        Left,   // The camera goes at the left of the trigger
        Right,  // The camera goes at the right of the trigger
    }
    
    [SerializeField] private MoveCamera cameraScript;
    [SerializeField] private bool showInGame = false;
    [SerializeField] private float time = 2;
    [SerializeField] private CameraDestination cameraDestination;

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
