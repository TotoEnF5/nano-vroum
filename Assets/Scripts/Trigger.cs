using Unity.VisualScripting;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    
    [SerializeField] private MoveCamera cameraScript;
    [SerializeField] private bool showInGame = false;
    [SerializeField] private float time = 2;
    [SerializeField] private MoveCamera.CameraDestination cameraDestination;
    [SerializeField] private Vector3 customPosition;
    [SerializeField, Min(0)] private float newCameraSize = 5;

    private void Awake()
    {
        GetComponent<Renderer>().enabled = showInGame;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (cameraDestination == MoveCamera.CameraDestination.Custom)
            {
                cameraScript.StartMovement(customPosition, newCameraSize, time);
            }
            else
            {
                cameraScript.StartMovement(transform, time, cameraDestination, newCameraSize);
            }
        }
    }
}
